using System;
using Generator.Generator;
using Newtonsoft.Json;


namespace Generator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var procedures = ProceduresGenerator.Generate(10);
            var diseases = DiseaseGenerator.Generate(40);
            var positions = PositionsGenerator.Generate(30);
            var workers = WorkersGenerator.Generate(5, positions);
            var vacationers = VacationerGenerator.Generate(5, workers);
            var diagnoses = DiagnosesGenerator.Generate(vacationers, diseases);
            var assignedProcedures = AssignedProceduresGenerator.Generate(vacationers, procedures);



            string prettyPositions = JsonConvert.SerializeObject(positions, Formatting.Indented);
            string prettyWorkers = JsonConvert.SerializeObject(workers, Formatting.Indented);
            string prettyVacationers = JsonConvert.SerializeObject(vacationers, Formatting.Indented);
            string prettyDiseases = JsonConvert.SerializeObject(diseases, Formatting.Indented);
            string prettyDiagnoses = JsonConvert.SerializeObject(diagnoses, Formatting.Indented);
            string prettyProcedures = JsonConvert.SerializeObject(procedures, Formatting.Indented);
            string prettyAssignedProcedures = JsonConvert.SerializeObject(assignedProcedures, Formatting.Indented);
            

            Console.WriteLine("Positions: \n" + prettyPositions);
            Console.WriteLine("Workers: \n" + prettyWorkers);
            Console.WriteLine("Vacationers: \n" + prettyVacationers);
            Console.WriteLine("Diagnoses: \n" + prettyDiagnoses);
            Console.WriteLine("Diseases: \n" + prettyDiseases);
            Console.WriteLine("Procedures: \n" + prettyProcedures);
            Console.WriteLine("AssignedProcedures: \n" + prettyAssignedProcedures);
        }
    }
}
