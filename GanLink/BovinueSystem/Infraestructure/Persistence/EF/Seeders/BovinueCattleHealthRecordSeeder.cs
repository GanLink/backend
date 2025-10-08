using System.Collections.Generic;
using GanLink.BovinueSystem.Domain.Models.Aggregates;

namespace GanLink.BovinueSystem.Infrastructure.Persistence.EF.Seeders
{
    public static class BovinueCattleHealthRecordSeeder
    {
        public static IEnumerable<object> GetData()
        {
            return new List<object>
            {
                new { 
                    Id = 1L,
                    ActivityName = "Official Identification (Eartagging)",
                    Frequency = 0,
                    Description = "Unique ear tag code is the animal's identity and traceability base"
                },
                new { 
                    Id = 2L,
                    ActivityName = "Foot-and-Mouth Disease Vaccination",
                    Frequency = 1,
                    Description = "Prevents highly contagious disease and ensures national sanitary control"
                },
                new { 
                    Id = 3L,
                    ActivityName = "Bovine Brucellosis Vaccination",
                    Frequency = 0,
                    Description = "Protects against reproductive disease, milk safety and genetic health (females 3-8 months)"
                },
                new { 
                    Id = 4L,
                    ActivityName = "Sanitation Tests",
                    Frequency = 1,
                    Description = "Herd diagnosis to control zoonotic and reproductive diseases (Brucellosis, TB)"
                },
                new { 
                    Id = 5L,
                    ActivityName = "Animal Movement Guide",
                    Frequency = 1,
                    Description = "Official document validating origin and sanitary status of the animal for each transfer"
                },
                new { 
                    Id = 6L,
                    ActivityName = "Free Herd Certificate Renewal",
                    Frequency = 1,
                    Description = "Certifies that the herd is free from brucellosis and/or tuberculosis (Milk Production)"
                },
                new { 
                    Id = 7L,
                    ActivityName = "Traceability and Detailed Records",
                    Frequency = 1,
                    Description = "Document origin, health and lineage for semen/embryo commercialization (Reproduction and Genetics)"
                }
            };
        }
    }
}