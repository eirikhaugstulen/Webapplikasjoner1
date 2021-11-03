using System;
using Xunit;
using System.Collections.Generic;
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
    public class StrekningerTest
    {
        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = "";
        

        private readonly Mock<IStrekningRepository> mockRep = new Mock<IStrekningRepository>();
        private readonly Mock<ILogger<StrekningController>> mockLog = new Mock<ILogger<StrekningController>>();

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();
        
        // Tester for Lagre Strekning i controller
        [Fact]
        public async Task LagreStrekningLoggInnOk()
        {
            // Arrange
            Strekning strekning = new Strekning()
            {
                StrekningNummer = "1",
                FraSted = "1",
                TilSted = "2",
            };
            
            mockRep.Setup(s => s.Lagre(strekning)).ReturnsAsync(true);

            var strekningController = new StrekningController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            strekningController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await strekningController.Lagre(strekning) as OkObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Strekning lagret", resultat.Value);
        }

        [Fact]
        public async Task LagreStrekningIkkeLoggetInn()
        {
            // Arrange
            mockRep.Setup(s => s.Lagre(It.IsAny<Strekning>())).ReturnsAsync(true);

            var strekningController = new StrekningController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            strekningController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await strekningController.Lagre(It.IsAny<Strekning>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }
        
        [Fact]
        public async Task LagreStrekningValideringFeilFraSted()
        {
            // Arrange
            Strekning strekning = new Strekning()
            {
                StrekningNummer = "8",
                FraSted = null,
                TilSted = "2",
            };
            
            mockRep.Setup(s => s.Lagre(strekning)).ReturnsAsync(true);

            var strekningController = new StrekningController(mockRep.Object, mockLog.Object);

            strekningController.ModelState.AddModelError("FraSted", "Feil i inputvalidering av strekning på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            strekningController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await strekningController.Lagre(strekning) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i validering av strekning", resultat.Value);
        }
        
        [Fact]
        public async Task LagreStrekningValideringFeilTilSted()
        {
            // Arrange
            Strekning strekning = new Strekning()
            {
                StrekningNummer = "8",
                FraSted = "2",
                TilSted = null,
            };
            
            mockRep.Setup(s => s.Lagre(strekning)).ReturnsAsync(true);

            var strekningController = new StrekningController(mockRep.Object, mockLog.Object);

            strekningController.ModelState.AddModelError("TilSted", "Feil i inputvalidering av strekning på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            strekningController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await strekningController.Lagre(strekning) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i validering av strekning", resultat.Value);
        }
        
        [Fact]
        public async Task LagreStrekningFeilIDb()
        {
            // Arrange
            Strekning strekning = new Strekning()
            {
                StrekningNummer = "1",
                FraSted = "2",
                TilSted = "3",
            };
            
            mockRep.Setup(k => k.Lagre(strekning)).ReturnsAsync(false);

            var strekningController = new StrekningController(mockRep.Object, mockLog.Object);
            

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            strekningController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await strekningController.Lagre(strekning) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Strekningen ble ikke lagret", resultat.Value);
        }

        // Tester for Endre Strekning i controller
        [Fact]
        public async Task EndreStrekningLoggInnOk()
        {
            // Arrange
            Strekning strekning = new Strekning()
            {
                StrekningNummer = "12",
                FraSted = "1",
                TilSted = "2",
            };
            
            mockRep.Setup(s => s.Endre((strekning))).ReturnsAsync(true);

            var strekningController = new StrekningController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            strekningController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await strekningController.Endre(strekning) as OkObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Strekning endret", resultat.Value);
        }
        
        [Fact]
        public async Task EndreStrekningIkkeLoggetInn()
        {
            // Arrange
            mockRep.Setup(s => s.Endre(It.IsAny<Strekning>())).ReturnsAsync(true);

            var strekningController = new StrekningController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            strekningController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await strekningController.Endre(It.IsAny<Strekning>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }
        
        [Fact]
        public async Task EndreStrekningValideringFeilFraSted()
        {
            // Arrange
            Strekning strekning = new Strekning()
            {
                StrekningNummer = "8",
                FraSted = null,
                TilSted = "2",
            };
            
            mockRep.Setup(s => s.Endre(strekning)).ReturnsAsync(true);

            var strekningController = new StrekningController(mockRep.Object, mockLog.Object);

            strekningController.ModelState.AddModelError("FraSted", "Feil i inputvalidering av strekning på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            strekningController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await strekningController.Endre(strekning) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i validering av strekning", resultat.Value);
        }
        
        [Fact]
        public async Task EndreStrekningValideringFeilTilSted()
        {
            // Arrange
            Strekning strekning = new Strekning()
            {
                StrekningNummer = "8",
                FraSted = "2",
                TilSted = null,
            };
            
            mockRep.Setup(s => s.Endre(strekning)).ReturnsAsync(true);

            var strekningController = new StrekningController(mockRep.Object, mockLog.Object);

            strekningController.ModelState.AddModelError("FraSted", "Feil i inputvalidering av strekning på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            strekningController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await strekningController.Endre(strekning) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i validering av strekning", resultat.Value);
        }
        [Fact]
        public async Task EndreStrekningFeilIDb()
        {
            // Arrange
            Strekning strekning = new Strekning()
            {
                StrekningNummer = "1",
                FraSted = "2",
                TilSted = "3",
            };
            
            mockRep.Setup(k => k.Endre(strekning)).ReturnsAsync(false);

            var strekningController = new StrekningController(mockRep.Object, mockLog.Object);
            

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            strekningController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await strekningController.Endre(strekning) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Strekningen ble ikke endret", resultat.Value);
        }
        
        // Tester for Slett Strekning
        [Fact]
        public async Task SlettStrekningLoggInnOk()
        {
            // Arrange
            mockRep.Setup(s => s.Slett(It.IsAny<string>())).ReturnsAsync(true);

            var strekningController = new StrekningController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            strekningController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await strekningController.Slett(It.IsAny<string>()) as OkObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Strekning slettet", resultat.Value);
        } 
        
        [Fact]
        public async Task SlettStrekningIkkeLoggetInn()
        {
            // Arrange
            mockRep.Setup(s => s.Slett(It.IsAny<string>())).ReturnsAsync(true);

            var strekningController = new StrekningController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            strekningController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await strekningController.Slett(It.IsAny<string>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        } 
        
        [Fact]
        public async Task SlettStrekningFeilIDB()
        {
            // Arrange
            mockRep.Setup(k => k.Slett(It.IsAny<string>())).ReturnsAsync(false);

            var strekningController = new StrekningController(mockRep.Object, mockLog.Object);
            

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            strekningController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await strekningController.Slett(It.IsAny<string>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Strekningen ble ikke slettet", resultat.Value);
        }
        
        // Tester for HentAlle Strekninger
        
        [Fact]
        public async Task HentAlleOk()
        {
            // Arrange
            var lokasjon1 = new Lokasjoner
            {
                StedsNummer = "1",
                Stedsnavn = "Oslo"
            };
            var lokasjon2 = new Lokasjoner
            {
                StedsNummer = "2",
                Stedsnavn = "Bergen"
            };
            
            
            var strekning1 = new Strekninger
            {
                StrekningNummer = "1",
                FraSted = lokasjon1,
                TilSted = lokasjon2,
            };
            var strekning2 = new Strekninger
            {
                StrekningNummer = "2",
                FraSted = lokasjon2,
                TilSted = lokasjon1,
            };

            var strekningListe = new List<Strekninger>();
            strekningListe.Add(strekning1);
            strekningListe.Add(strekning2);
            
            mockRep.Setup(k => k.HentAlle()).ReturnsAsync(strekningListe);

            var strekningController = new StrekningController(mockRep.Object, mockLog.Object);

            // Act
            var resultat = await strekningController.HentAlle() as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal(strekningListe, resultat.Value);
        }
        
        // Tester for HentEn Strekning
        
        [Fact]
        public async Task HentEnOk()
        {
            // Arrange
            var lokasjon1 = new Lokasjoner
            {
                StedsNummer = "1",
                Stedsnavn = "Oslo"
            };
            var lokasjon2 = new Lokasjoner
            {
                StedsNummer = "2",
                Stedsnavn = "Bergen"
            };
            
            
            var strekning = new Strekninger
            {
                StrekningNummer = "1",
                FraSted = lokasjon1,
                TilSted = lokasjon2,
            };
            
            mockRep.Setup(s => s.HentEn(It.IsAny<string>())).ReturnsAsync(strekning);

            var kundeController = new StrekningController(mockRep.Object, mockLog.Object);
            

            // Act
            var resultat = await kundeController.HentEn(It.IsAny<string>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<Strekninger>(strekning,(Strekninger)resultat.Value);
        }
        
        [Fact]
        public async Task HentEnFeilIDB()
        {
            // Arrange
            mockRep.Setup(s => s.HentEn(It.IsAny<string>())).ReturnsAsync(()=>null);

            var kundeController = new StrekningController(mockRep.Object, mockLog.Object);
            

            // Act
            var resultat = await kundeController.HentEn(It.IsAny<string>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Fant ikke strekningen",resultat.Value);

        }
       
        
    }
}