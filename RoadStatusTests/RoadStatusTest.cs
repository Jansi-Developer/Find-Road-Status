using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoadStatusAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace RoadStatusTest.Tests
{
    [TestClass()]
    public class RoadStatusTest
    {

        [TestMethod()]
        public async Task GetValidRoadStatus1_Test()
        {
            List<RoadDetails> testRoadDetails = GetTestRoadDetails();
            
            RoadStatus roadStatus = new RoadStatus(new string[] { testRoadDetails[0].ID });
            List<RoadDetails> roadDetails = await roadStatus.RoadStatusRequest();

            Assert.IsNotNull(roadDetails);
            Assert.AreEqual(testRoadDetails[0].Name, roadDetails[0].Name);
        }

        [TestMethod()]
        public async Task GetValidRoadStatus2_Test()
        {
            List<RoadDetails> testRoadDetails = GetTestRoadDetails();

            RoadStatus roadStatus = new RoadStatus(new string[] { testRoadDetails[1].ID });
            List<RoadDetails> roadDetails = await roadStatus.RoadStatusRequest();

            Assert.IsNotNull(roadDetails);
            Assert.AreEqual(testRoadDetails[1].Status, roadDetails[0].Status);
        }

        [TestMethod()]
        public async Task GetInValidRoadStatus_Test()
        {
            List<RoadDetails> testRoadDetails = GetTestRoadDetails();

            RoadStatus roadStatus = new RoadStatus(new string[] { testRoadDetails[2].ID });
            List<RoadDetails> roadDetails = await roadStatus.RoadStatusRequest();

            Assert.IsNull(roadDetails);       
        }

        private List<RoadDetails> GetTestRoadDetails()
        {
            var testRoadDetails = new List<RoadDetails>();
            testRoadDetails.Add(new RoadDetails { ID = "A1", Name = "A1", Status = "Closure", StatusDescription = "Closure" });
            testRoadDetails.Add(new RoadDetails { ID = "A12", Name = "A12", Status = "Good", StatusDescription = "No Exceptional Delays" });
            testRoadDetails.Add(new RoadDetails { ID = "A35", Name = "A35", Status = "Good", StatusDescription = "No Exceptional Delays" });

            return testRoadDetails;
        }
    }
}