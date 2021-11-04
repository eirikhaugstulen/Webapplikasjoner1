using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Webapplikasjoner1.Controllers;
using Webapplikasjoner1.DAL;
using Webapplikasjoner1.Models;
using Xunit;

namespace WebAppTest
{
    public class KundeTest
    {
        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = "";
        

        private readonly Mock<IKundeRepository> mockRep = new Mock<IKundeRepository>();
        private readonly Mock<ILogger<KundeController>> mockLog = new Mock<ILogger<KundeController>>();

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();
        
        // Tester for Lagre kunde
        [Fact]
        public async Task LagreOk()
        {
            // Arrange
            Kunde kunde = new Kunde()
            {
                KundeId = "abcde",
                Fornavn = "Per",
                Etternavn = "Larsen",
                Adresse = "Pilestredet 10",
                Epost = "per@outlook.com",
                Telefonnummer = "004748261723"
            };
            mockRep.Setup(k => k.Lagre(kunde)).ReturnsAsync(true);

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            // Act
            var resultat = await kundeController.Lagre(kunde) as OkObjectResult;
            // Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Kunde lagret", resultat.Value);
        }
        [Fact]
        public async Task LagreFeilIDb()
        {
            // Arrange
            Kunde kunde = new Kunde()
            {
                KundeId = "abcde",
                Fornavn = "Per",
                Etternavn = "Larsen",
                Adresse = "Pilestredet 10",
                Epost = "per@outlook.com",
                Telefonnummer = "004748261723"
            };
            mockRep.Setup(k => k.Lagre(kunde)).ReturnsAsync(false);

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            
            // Act
            var resultat = await kundeController.Lagre(kunde) as BadRequestObjectResult;
            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Kunde ble ikke lagret", resultat.Value);
        }
        [Fact]
        public async Task LagreFeilFornavn()
        {
            // Arrange
            Kunde kunde = new Kunde()
            {
                KundeId = "abcde",
                Fornavn = "F",
                Etternavn = "Larsen",
                Adresse = "Pilestredet 10",
                Epost = "per@outlook.com",
                Telefonnummer = "004748261723"
            };
            mockRep.Setup(k => k.Lagre(kunde)).ReturnsAsync(true);

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await kundeController.Lagre(kunde) as BadRequestObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i validering av kunde", resultat.Value);
        }
        
        [Fact]
        public async Task LagreFeilEtternavn()
        {
            // Arrange
            Kunde kunde = new Kunde()
            {
                KundeId = "abcde",
                Fornavn = "Per",
                Etternavn = "F",
                Adresse = "Pilestredet 10",
                Epost = "per@outlook.com",
                Telefonnummer = "004748261723"
            };
            mockRep.Setup(k => k.Lagre(kunde)).ReturnsAsync(true);

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;
            // Act
            var resultat = await kundeController.Lagre(kunde) as BadRequestObjectResult;
            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i validering av kunde", resultat.Value);
        }
        [Fact]
        public async Task LagreFeilAdresse()
        {
            // Arrange
            Kunde kunde = new Kunde()
            {
                KundeId = "abcde",
                Fornavn = "Per",
                Etternavn = "Larsen",
                Adresse = "P",
                Epost = "per@outlook.com",
                Telefonnummer = "004748261723"
            };
            mockRep.Setup(k => k.Lagre(kunde)).ReturnsAsync(true);

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await kundeController.Lagre(kunde) as BadRequestObjectResult;
            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i validering av kunde", resultat.Value);
        }
        
        [Fact]
        public async Task LagreFeilEpost()
        {
            // Arrange
            Kunde kunde = new Kunde()
            {
                KundeId = "abcde",
                Fornavn = "Per",
                Etternavn = "Larsen",
                Adresse = "Pilestredet 10",
                Epost = "FeilEpost",
                Telefonnummer = "004748261723"
            };
            mockRep.Setup(k => k.Lagre(kunde)).ReturnsAsync(true);

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await kundeController.Lagre(kunde) as BadRequestObjectResult;
            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i validering av kunde", resultat.Value);
        }
        [Fact]
        public async Task LagreFeilTelefonnummer()
        {
            // Arrange
            Kunde kunde = new Kunde()
            {
                KundeId = "abcde",
                Fornavn = "Per",
                Etternavn = "Larsen",
                Adresse = "Pilestredet 10",
                Epost = "per@outlook.com",
                Telefonnummer = "123456"
            };
            mockRep.Setup(k => k.Lagre(kunde)).ReturnsAsync(true);

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await kundeController.Lagre(kunde) as BadRequestObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i validering av kunde", resultat.Value);
        }
        
        // Tester for slett Kunde
        [Fact]
        public async Task SlettOk()
        {
            // Arrange
            mockRep.Setup(k => k.Slett(It.IsAny<string>())).ReturnsAsync(true);

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await kundeController.Slett(It.IsAny<string>()) as OkObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Kunde ble slettet", resultat.Value);
        }
        
        [Fact]
        public async Task SlettIkkeOk()
        {
            // Arrange
            mockRep.Setup(k => k.Slett(It.IsAny<string>())).ReturnsAsync(false);

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await kundeController.Slett(It.IsAny<string>()) as NotFoundObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Kunde ble ikke slettet", resultat.Value);
        }
        
        
    }
}