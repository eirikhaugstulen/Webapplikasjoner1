using System.Collections.Generic;
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
            var resultat = await lokController.Registrer(It.IsAny<Lokasjon>()) as UnauthorizedObjectResult;
            
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
                StedsNummer = "1",
                Stedsnavn = "Fredrikstad",
            };
            mockRep.Setup(l => l.RegistrerLokasjon(It.IsAny<Lokasjon>())).ReturnsAsync(true);
            var lokController = new LokasjonController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            lokController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await lokController.Registrer(lokasjon) as OkObjectResult;
            
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
                StedsNummer = "1",
                Stedsnavn = "E", // Ugyldig stedsnavn
            };

            mockRep.Setup(l => l.RegistrerLokasjon(It.IsAny<Lokasjon>())).ReturnsAsync(true);
            var lokController = new LokasjonController(mockRep.Object, mockLog.Object);
            lokController.ModelState.AddModelError("Stedsnavn","Feil i inputvalidering");
            
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            lokController.ControllerContext.HttpContext = mockHttpContext.Object;
            // Act
            var resultat = await lokController.Registrer(lokasjon) as BadRequestObjectResult;

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
                StedsNummer = "1",
                Stedsnavn = "Fredrikstad",
            };
            
            mockRep.Setup(l => l.RegistrerLokasjon(It.IsAny<Lokasjon>())).ReturnsAsync(false);
            var lokController = new LokasjonController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            lokController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await lokController.Registrer(lokasjon) as BadRequestObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Lokasjonen ble ikke lagret", resultat.Value);
        }

        [Fact]
        public async Task SlettLokasjonIkkeLoggetInn()
        {
            // Arrange
            mockRep.Setup(l => l.SlettLokasjon(It.IsAny<string>())).ReturnsAsync(true);
            var lokController = new LokasjonController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            lokController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await lokController.Slett(It.IsAny<string>()) as UnauthorizedObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task SlettLokasjonLoggetInnOK()
        {
            mockRep.Setup(l => l.SlettLokasjon(It.IsAny<string>())).ReturnsAsync(true);
            var lokController = new LokasjonController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            lokController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await lokController.Slett(It.IsAny<string>()) as OkObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Lokasjonen ble slettet", resultat.Value);
        }
        
        [Fact]
        public async Task SlettLokasjonFeilIDb()
        {
            mockRep.Setup(l => l.SlettLokasjon(It.IsAny<string>())).ReturnsAsync(false);
            var lokController = new LokasjonController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            lokController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await lokController.Slett(It.IsAny<string>()) as NotFoundObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Lokasjonen ble ikke slettet", resultat.Value);
        }

        [Fact]
        public async Task HentAlleLokasjonerOk()
        {
            // Arrange
            List<Lokasjon> alleLokasjoner = new List<Lokasjon>();
            Lokasjon lokasjon = new Lokasjon()
            {
                StedsNummer = "1",
                Stedsnavn = "Fredrikstad",
            };
            
            Lokasjon lokasjon2 = new Lokasjon()
            {
                StedsNummer = "2",
                Stedsnavn = "Bergen",
            };
            
            alleLokasjoner.Add(lokasjon);
            alleLokasjoner.Add(lokasjon2);
            
            mockRep.Setup(l => l.HentAlle()).ReturnsAsync(alleLokasjoner);
            var lokController = new LokasjonController(mockRep.Object, mockLog.Object);

            // Act
            var resultat = await lokController.HentAlle() as OkObjectResult;

            // Assert
            Assert.Equal<List<Lokasjon>>((List<Lokasjon>)resultat.Value,alleLokasjoner);
        }

        [Fact]
        public async Task HentEnLokasjonOk()
        {
            // Arrange
            var lokasjon = new Lokasjon()
            {
                StedsNummer = "1",
                Stedsnavn = "Bergen",
            };

            mockRep.Setup(l => l.HentEn(It.IsAny<string>())).ReturnsAsync(lokasjon);
            var lokController = new LokasjonController(mockRep.Object, mockLog.Object);
            
            // Act
            var resultat = await lokController.HentEn(It.IsAny<string>()) as OkObjectResult;

            // Assert
            Assert.Equal<Lokasjon>(lokasjon, (Lokasjon)resultat.Value);
        }

        [Fact]
        public async Task HentEnLokasjonNotFound()
        {
            // Arrange
            mockRep.Setup(l => l.HentEn(It.IsAny<string>())).ReturnsAsync(() => null);
            var lokController = new LokasjonController(mockRep.Object, mockLog.Object);
            
            // Act
            var resultat = await lokController.HentEn(It.IsAny<string>()) as NotFoundObjectResult;

            // Assert
            Assert.Equal("Fant ikke lokasjonen", resultat.Value);
        }
    }
}