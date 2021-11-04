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
using Xunit;

namespace WebAppTest
{
    public class AvgangerTest
    {
        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = "";
        

        private readonly Mock<IAvgangerRepository> mockRep = new Mock<IAvgangerRepository>();
        private readonly Mock<ILogger<AvgangerController>> mockLog = new Mock<ILogger<AvgangerController>>();

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();
        
        //Tester lagreAvgang
        
        [Fact]
        public async Task LagreAvgangLoggInnOk()
        {
            // Arrange
            Avgang avgang= new Avgang()
            {  
                AvgangNummer = "1",
                Dato = "10-12-2021",
                Klokkeslett = "23:59",
                Pris = 100,
                Strekning = "12"
            };
            
            mockRep.Setup(a => a.Lagre(avgang)).ReturnsAsync(true);

            var avgangerController = new AvgangerController(mockRep.Object,mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangerController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await avgangerController.Lagre(avgang) as OkObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Avgangen lagret", resultat.Value);
        }
        
        [Fact]
        public async Task LagreAvgangIkkeLoggetInn()
        {
            // Arrange
            mockRep.Setup(a => a.Lagre(It.IsAny<Avgang>())).ReturnsAsync(true);
            var avgangerController = new AvgangerController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangerController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await avgangerController.Lagre(It.IsAny<Avgang>()) as UnauthorizedObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }
        
        [Fact]
        public async Task LagreAvgangFeilInput1()
        {
            // Arrange 
            Avgang avgang= new Avgang()
            {  
                AvgangNummer = "1",
                Dato = "10-12-2021",
                Klokkeslett = null,
                Pris = 100,
                Strekning = "12"
            };

            mockRep.Setup(a => a.Lagre(avgang)).ReturnsAsync(true);
            var avgangerController = new AvgangerController(mockRep.Object, mockLog.Object);
            avgangerController.ModelState.AddModelError("Klokkeslett","Feil i inputvalidering");
            
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangerController.ControllerContext.HttpContext = mockHttpContext.Object;
            // Act
            var resultat = await avgangerController.Lagre(avgang) as BadRequestObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest,resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering", resultat.Value);
        }
        
        [Fact]
        public async Task LagreAvgangFeilInput2()
        {
            // Arrange 
            Avgang avgang= new Avgang()
            {  
                AvgangNummer = "1",
                Dato = null,
                Klokkeslett = "23:59",
                Pris = 100,
                Strekning = "12"
            };

            mockRep.Setup(a => a.Lagre(avgang)).ReturnsAsync(true);
            var avgangerController = new AvgangerController(mockRep.Object, mockLog.Object);
            avgangerController.ModelState.AddModelError("Dato","Feil i inputvalidering");
            
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangerController.ControllerContext.HttpContext = mockHttpContext.Object;
            // Act
            var resultat = await avgangerController.Lagre(avgang) as BadRequestObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest,resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering", resultat.Value);
        }
        
        [Fact]
        public async Task LagreAvgangFeilInput3()
        {
            // Arrange 
            Avgang avgang= new Avgang()
            {  
                AvgangNummer = "1",
                Dato = "10-12-2021",
                Klokkeslett = "23:59",
                Pris = -2000,
                Strekning = "12"
            };

            mockRep.Setup(a => a.Lagre(avgang)).ReturnsAsync(true);
            var avgangerController = new AvgangerController(mockRep.Object, mockLog.Object);
            avgangerController.ModelState.AddModelError("Pris","Feil i inputvalidering");
            
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangerController.ControllerContext.HttpContext = mockHttpContext.Object;
            // Act
            var resultat = await avgangerController.Lagre(avgang) as BadRequestObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest,resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering", resultat.Value);
        }
        
        [Fact]
        public async Task LagreAvgangFeilInput4()
        {
            // Arrange 
            Avgang avgang= new Avgang()
            {  
                AvgangNummer = "1",
                Dato = "10-12-2021",
                Klokkeslett = "23:59",
                Pris = 100,
                Strekning = null
            };

            mockRep.Setup(a => a.Lagre(avgang)).ReturnsAsync(true);
            var avgangerController = new AvgangerController(mockRep.Object, mockLog.Object);
            avgangerController.ModelState.AddModelError("Strekning","Feil i inputvalidering");
            
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangerController.ControllerContext.HttpContext = mockHttpContext.Object;
            // Act
            var resultat = await avgangerController.Lagre(avgang) as BadRequestObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest,resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering", resultat.Value);
        }
        
        [Fact]
        public async Task LagreAvgangFeilIDb()
        {
            // Arrange
            Avgang avgang= new Avgang()
            {  
                AvgangNummer = "1",
                Dato = "10-12-2021",
                Klokkeslett = "23:59",
                Pris = 100,
                Strekning = "12"
            };
            
            mockRep.Setup(a => a.Lagre(It.IsAny<Avgang>())).ReturnsAsync(false);
            var avgangerController = new AvgangerController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangerController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await avgangerController.Lagre(avgang) as BadRequestObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Avgangen ble ikke lagret", resultat.Value);
        }
        
        
        // Tester for slett avgang
        [Fact]
        public async Task SlettAvgangIkkeLoggetInn()
        {
            // Arrange
            mockRep.Setup(a => a.Slett(It.IsAny<string>())).ReturnsAsync(true);
            var avgangerController = new AvgangerController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangerController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await avgangerController.Slett(It.IsAny<string>()) as UnauthorizedObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task SlettAvgangLoggetInnOK()
        {
            // Arrange
            mockRep.Setup(l => l.Slett(It.IsAny<string>())).ReturnsAsync(true);
            var avgangerController = new AvgangerController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangerController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await avgangerController.Slett(It.IsAny<string>()) as OkObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Avgang slettet", resultat.Value);
        }
        
        [Fact]
        public async Task SlettAvgangFeilIDb()
        {
            // Arrange
            mockRep.Setup(a => a.Slett(It.IsAny<string>())).ReturnsAsync(false);
            var avgangerController = new AvgangerController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangerController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await avgangerController.Slett(It.IsAny<string>()) as NotFoundObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Avgangen ble ikke slettet", resultat.Value);
        }
     
        // Tester for hent alle avganger
        [Fact]
        public async Task HentAlleAvgangerOk()
        {
            // Arrange
            List<Avganger> alleAvganger = new List<Avganger>();

            
            var lokasjon = new Lokasjoner()
            {
                StedsNummer = "1",
                Stedsnavn = "Oslo",
            };
            
            var lokasjon2 = new Lokasjoner()
            {
                StedsNummer = "2",
                Stedsnavn = "Bergen",
            };
            
            var strekning = new Strekninger()
            {
                StrekningNummer = "12",
                FraSted = lokasjon,
                TilSted = lokasjon2,
            };
            
            var strekning1 = new Strekninger()
            {
                StrekningNummer = "123",
                FraSted = lokasjon,
                TilSted = lokasjon2,
            };

            Avganger avgang= new Avganger()
            {  
                AvgangNummer = "1",
                Dato = "10-12-2021",
                Klokkeslett = "23:59",
                Pris = 100,
                Strekning= strekning
            };
            
            Avganger avgang2= new Avganger()
            {  
                AvgangNummer = "2",
                Dato = "11-12-2021",
                Klokkeslett = "23:40",
                Pris = 100,
                Strekning = strekning1
            };
            
            alleAvganger.Add(avgang);
            alleAvganger.Add(avgang2);
            
            mockRep.Setup(a => a.HentAlle()).ReturnsAsync(alleAvganger);
            var avgangerController = new AvgangerController(mockRep.Object, mockLog.Object);

            // Act
            var resultat = await avgangerController.HentAlle() as OkObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<List<Avganger>>((List<Avganger>)resultat.Value,alleAvganger);
        }
        
        // Tester for hent en avgang
        [Fact]
        public async Task HentEnAvgangOk()
        {
            // Arrange
            var hentetAvgang = new Avganger()
            {  
                AvgangNummer = "1",
                Dato = "10-12-2021",
                Klokkeslett = "23:59",
                Pris = 100,
            };

            mockRep.Setup(a => a.HentEn(It.IsAny<string>())).ReturnsAsync(hentetAvgang);
            var avgangerController = new AvgangerController(mockRep.Object, mockLog.Object);
            
            // Act
            var resultat = await avgangerController.HentEn(It.IsAny<string>()) as OkObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<Avganger>(hentetAvgang, (Avganger)resultat.Value);
        }
        
        [Fact]
        public async Task HentEnAvgangNotFound()
        {
            // Arrange
            mockRep.Setup(a => a.HentEn(It.IsAny<string>())).ReturnsAsync(() => null);
            var avgangerController = new AvgangerController(mockRep.Object, mockLog.Object);
            
            // Act
            var resultat = await avgangerController.HentEn(It.IsAny<string>()) as NotFoundObjectResult;

            // Assert
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Fant ikke avgangen", resultat.Value);
        }
        
        // Tester for endre avgang
        [Fact]
        public async Task EndreAvgangLoggInnOk()
        {
            // Arrange
            Avgang avgang= new Avgang()
            {  
                AvgangNummer = "1",
                Dato = "10-12-2021",
                Klokkeslett = "23:59",
                Pris = 100,
                Strekning = "12"
            };
            
            mockRep.Setup(s => s.Endre((avgang))).ReturnsAsync(true);

            var avgangerController = new AvgangerController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangerController.ControllerContext.HttpContext = mockHttpContext.Object;
            
            // Act
            var resultat = await avgangerController.Endre(avgang) as OkObjectResult;
            
            // Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Avgangen ble endret", resultat.Value);
        }
        
        [Fact]
        public async Task EndreAvgangIkkeLoggetInn()
        {
            // Arrange
            mockRep.Setup(s => s.Endre(It.IsAny<Avgang>())).ReturnsAsync(true);

            var avgangerController = new AvgangerController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangerController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await avgangerController.Endre(It.IsAny<Avgang>()) as UnauthorizedObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }
        
        [Fact]
        public async Task EndreAvgangFeilInput1()
        {
            // Arrange
            Avgang avgang= new Avgang()
            {  
                AvgangNummer = "1",
                Dato = "10-12-2021",
                Klokkeslett = null,
                Pris = 100,
                Strekning = "12"
            };
            
            mockRep.Setup(s => s.Endre(avgang)).ReturnsAsync(true);

            var avgangerController = new AvgangerController(mockRep.Object, mockLog.Object);

            avgangerController.ModelState.AddModelError("Klokkeslett", "Feil i inputvalidering");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangerController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await avgangerController.Endre(avgang) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering", resultat.Value);
        }
        
        [Fact]
        public async Task EndreAvgangFeilInput2()
        {
            // Arrange
            Avgang avgang= new Avgang()
            {  
                AvgangNummer = "1",
                Dato = null,
                Klokkeslett = "23:59",
                Pris = 100,
                Strekning = "12"
            };
            
            mockRep.Setup(s => s.Endre(avgang)).ReturnsAsync(true);

            var avgangerController = new AvgangerController(mockRep.Object, mockLog.Object);

            avgangerController.ModelState.AddModelError("Dato", "Feil i inputvalidering");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangerController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await avgangerController.Endre(avgang) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering", resultat.Value);
        }
        
        [Fact]
        public async Task EndreAvgangFeilInput3()
        {
            // Arrange
            Avgang avgang= new Avgang()
            {  
                AvgangNummer = "1",
                Dato = "10-12-2021",
                Klokkeslett = "23:59",
                Pris = -100,
                Strekning = "12"
            };
            
            mockRep.Setup(s => s.Endre(avgang)).ReturnsAsync(true);

            var avgangerController = new AvgangerController(mockRep.Object, mockLog.Object);

            avgangerController.ModelState.AddModelError("Pris", "Feil i inputvalidering");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangerController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await avgangerController.Endre(avgang) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering", resultat.Value);
        }
        
        [Fact]
        public async Task EndreAvgangFeilInput4()
        {
            // Arrange
            Avgang avgang= new Avgang()
            {  
                AvgangNummer = "1",
                Dato = "10-12-2021",
                Klokkeslett = "23:59",
                Pris = 100,
                Strekning = null
            };
            
            mockRep.Setup(s => s.Endre(avgang)).ReturnsAsync(true);

            var avgangerController = new AvgangerController(mockRep.Object, mockLog.Object);

            avgangerController.ModelState.AddModelError("Strekning", "Feil i inputvalidering");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangerController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await avgangerController.Endre(avgang) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering", resultat.Value);
        }
        
        [Fact]
        public async Task EndreAvgangFeilIDb()
        {
            // Arrange
            Avgang avgang= new Avgang()
            {  
                AvgangNummer = "1",
                Dato = "10-12-2021",
                Klokkeslett = "23:59",
                Pris = 100,
                Strekning = "12"
            };
            
            mockRep.Setup(k => k.Endre(avgang)).ReturnsAsync(false);

            var avgangerController = new AvgangerController(mockRep.Object, mockLog.Object);
            

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            avgangerController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await avgangerController.Endre(avgang) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Avgangen ble ikke endret", resultat.Value);
        }
    }
}