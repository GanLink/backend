using GanLink.BovinueSystem.Domain.Models.Aggregates;

namespace GanLink.BovinueSystem.Infraestructure.Persistence.EF.Seeders
{
    public static class BovinueMetricParameterSeeder
    {
        public static IEnumerable<BovinueMetricParameter> GetData()
        {
            return new List<BovinueMetricParameter>
            {
                // Productive (Milk) - CategoryId = 1
                new()
                {
                    Id = 1,
                    CategoryId = 1,
                    Parameter = "Milk Production Per Day",
                    Description = "Individual yield level in liters"
                },
                new()
                {
                    Id = 2,
                    CategoryId = 1,
                    Parameter = "Fat and Protein Content",
                    Description = "Milk nutritional quality in percentage"
                },
                new()
                {
                    Id = 3,
                    CategoryId = 1,
                    Parameter = "Somatic Cell Count (SCC)",
                    Description = "Udder health and mastitis presence indicator"
                },

                // Productive (Meat) - CategoryId = 2
                new()
                {
                    Id = 4,
                    CategoryId = 2,
                    Parameter = "Average Daily Gain (ADG)",
                    Description = "Growth and fattening efficiency in kg/day"
                },
                new()
                {
                    Id = 5,
                    CategoryId = 2,
                    Parameter = "Weaning Weight",
                    Description = "Initial calf development in kg"
                },
                new()
                {
                    Id = 6,
                    CategoryId = 2,
                    Parameter = "Carcass Yield (%)",
                    Description = "Proportion of useful meat after slaughter"
                },

                // Feed Efficiency - CategoryId = 3
                new()
                {
                    Id = 7,
                    CategoryId = 3,
                    Parameter = "Dry Matter Intake (DMI)",
                    Description = "Amount of feed consumed in kg"
                },
                new()
                {
                    Id = 8,
                    CategoryId = 3,
                    Parameter = "Feed Conversion Ratio",
                    Description = "Efficiency: kg of feed needed to gain 1 kg of weight"
                },

                // Reproductive/Pregnancy - CategoryId = 4
                new()
                {
                    Id = 9,
                    CategoryId = 4,
                    Parameter = "Pregnancy Rate",
                    Description = "Percentage of cows that become pregnant"
                },
                new()
                {
                    Id = 10,
                    CategoryId = 4,
                    Parameter = "Conception Rate",
                    Description = "Pregnancy success relative to services performed"
                },
                new()
                {
                    Id = 11,
                    CategoryId = 4,
                    Parameter = "Calving Interval",
                    Description = "Days between calving and new pregnancy (affects annual productivity)"
                },
                new()
                {
                    Id = 12,
                    CategoryId = 4,
                    Parameter = "Services Per Conception",
                    Description = "Average number of inseminations or matings to achieve pregnancy"
                },

                // Genetic - CategoryId = 5
                new()
                {
                    Id = 13,
                    CategoryId = 5,
                    Parameter = "Family Tree/Pedigree",
                    Description = "Lineage record and inbreeding control"
                },
                new()
                {
                    Id = 14,
                    CategoryId = 5,
                    Parameter = "Genetic Merit Index",
                    Description = "Estimated genetic value for production, health or fertility"
                },

                // Sanitary - CategoryId = 6
                new()
                {
                    Id = 15,
                    CategoryId = 6,
                    Parameter = "Brucellosis and Tuberculosis Incidence",
                    Description = "Control of high-impact zoonotic diseases"
                },
                new()
                {
                    Id = 16,
                    CategoryId = 6,
                    Parameter = "Mastitis Incidence",
                    Description = "Frequency of infections in dairy cows"
                },
                new()
                {
                    Id = 17,
                    CategoryId = 6,
                    Parameter = "Body Condition Score (BCS)",
                    Description = "Nutritional status and energy balance of the animal (scale 1-5)"
                }
            };
        }
    }
}
