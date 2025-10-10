// BovinueCattleHealthRecordSeeder.cs
using GanLink.BovinueSystem.Domain.Models.Aggregates;

namespace GanLink.BovinueSystem.Infraestructure.Persistence.EF.Seeders
{
    public static class BovinueCattleHealthRecordSeeder
    {
        public static IEnumerable<BovinueCattleHealthRecord> GetData()
        {
            return new List<BovinueCattleHealthRecord>
            {
                new() { Id = 1, ActivityName = "Official Identification (Eartagging)", Frequency = 0, Description = "Unique ear tag code is the animal's identity and traceability base" },
                new() { Id = 2, ActivityName = "Foot-and-Mouth Disease Vaccination",   Frequency = 1, Description = "Prevents highly contagious disease and ensures national sanitary control" },
                new() { Id = 3, ActivityName = "Bovine Brucellosis Vaccination",       Frequency = 0, Description = "Protects against reproductive disease, milk safety and genetic health (females 3-8 months)" },
                new() { Id = 4, ActivityName = "Sanitation Tests",                      Frequency = 1, Description = "Herd diagnosis to control zoonotic and reproductive diseases (Brucellosis, TB)" },
                new() { Id = 5, ActivityName = "Animal Movement Guide",                 Frequency = 1, Description = "Official document validating origin and sanitary status of the animal for each transfer" },
                new() { Id = 6, ActivityName = "Free Herd Certificate Renewal",         Frequency = 1, Description = "Certifies that the herd is free from brucellosis and/or tuberculosis (Milk Production)" },
                new() { Id = 7, ActivityName = "Traceability and Detailed Records",     Frequency = 1, Description = "Document origin, health and lineage for semen/embryo commercialization (Reproduction and Genetics)" }
            };
        }
    }
}