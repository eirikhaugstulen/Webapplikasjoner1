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


        }
        
        // Tester for HentEn Strekning
        
        [Fact]
        public async Task HentEnOk()
        {


        }
        
        [Fact]
        public async Task HentEnFeilIDB()
        {


        }
       
        
    }
}