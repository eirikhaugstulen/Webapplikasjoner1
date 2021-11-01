using Xunit;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Webapplikasjoner1.Controllers;
using Webapplikasjoner1.DAL;
using Webapplikasjoner1.Models;

namespace WebAppTest
{
    public class LokasjonTest
    {
        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = "";
        
        private readonly Mock<ILokasjonRepository> mockRep = new Mock<ILokasjonRepository>();
        private readonly Mock<ILogger<LokasjonController>> mockLog = new Mock<ILogger<LokasjonController>>();

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();

        [Fact]
        public async Task RegistrerLokasjonIkkeLoggetInn()
        {
            // Arrange
            mockRep.Setup(l => l.RegistrerLokasjon(It.IsAny<Lokasjon>())).ReturnsAsync(true);
            var lokController = new LokasjonController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            lokController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await lokController.RegistrerLokasjon(It.IsAny<Lokasjon>()) as UnauthorizedObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }
        
        [Fact]
        public async Task RegistrerLokasjonLoggetInnOk()
        {
            // Arrange
            Lokasjon lokasjon = new Lokasjon()
            {
                Id = 1,
                Stedsnavn = "Fredrikstad",
            };
            mockRep.Setup(l => l.RegistrerLokasjon(It.IsAny<Lokasjon>())).ReturnsAsync(true);
            var lokController = new LokasjonController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            lokController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await lokController.RegistrerLokasjon(lokasjon) as OkObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Lokasjon lagret", resultat.Value);
        }

        [Fact]
        public async Task RegistrerLokasjonFeilInput()
        {
            // Arrange 
            Lokasjon lokasjon = new Lokasjon()
            {
                Id = 1,
                Stedsnavn = "E", // Ugyldig stedsnavn
            };

            mockRep.Setup(l => l.RegistrerLokasjon(It.IsAny<Lokasjon>())).ReturnsAsync(true);
            var lokController = new LokasjonController(mockRep.Object, mockLog.Object);
            lokController.ModelState.AddModelError("Stedsnavn","Feil i inputvalidering");
            
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            lokController.ControllerContext.HttpContext = mockHttpContext.Object;
            // Act
            var resultat = await lokController.RegistrerLokasjon(lokasjon) as BadRequestObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest,resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering", resultat.Value);
        }

        [Fact]
        public async Task RegistrerLokasjonFeilIDb()
        {
            // Arrange
            Lokasjon lokasjon = new Lokasjon()
            {
                Id = 1,
                Stedsnavn = "Fredrikstad",
            };
            
            mockRep.Setup(l => l.RegistrerLokasjon(It.IsAny<Lokasjon>())).ReturnsAsync(false);
            var lokController = new LokasjonController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            lokController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await lokController.RegistrerLokasjon(lokasjon) as BadRequestObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Lokasjonen ble ikke lagret", resultat.Value);
        }
    }
}