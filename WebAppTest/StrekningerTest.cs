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
            mockRep.Setup(s => s.LagreStrekning(It.IsAny<Strekning>())).ReturnsAsync(true);

            var strekningController = new StrekningController(mockRep.Object, mockLog.Object);
            
        }

        [Fact]
        public async Task LagreStrekningIkkeLoggetInn()
        {
         
        }
        
        [Fact]
        public async Task LagreStrekningValideringFeil()
        {
       
        }
        
        [Fact]
        public async Task LagreStrekningFeilIDb()
        {
           
        }

        // Tester for Endre Strekning i controller
        [Fact]
        public async Task EndreStrekningLoggInnOk()
        {


        }
        
        [Fact]
        public async Task EndreStrekningIkkeLoggetInn()
        {


        }
        
        [Fact]
        public async Task EndreStrekningValideringFeil()
        {


        }
        [Fact]
        public async Task EndreStrekningFeilIDb()
        {


        }
        
        // Tester for Slett Strekning
        [Fact]
        public async Task SlettStrekningLoggInnOk()
        {


        } 
        
        [Fact]
        public async Task SlettStrekningIkkeLoggetInn()
        {


        } 
        
        [Fact]
        public async Task SlettStrekningFeilIDB()
        {


        }
        
        // Tester for HentAlle Strekninger
        
        [Fact]
        public async Task HentAlleLoggetInnOk()
        {


        }
        
        [Fact]
        public async Task HentAlleIkkeLoggetInn()
        {


        }
        // Tester for HentEn Strekning
        
        [Fact]
        public async Task HentEnIkkeLoggetInn()
        {


        }
        [Fact]
        public async Task HentEnLoggetInnOk()
        {


        }
        
        [Fact]
        public async Task HentEnFeilIDB()
        {


        }
       
        
    }
}