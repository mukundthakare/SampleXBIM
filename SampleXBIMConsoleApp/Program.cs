using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.ModelGeometry.Scene;

namespace SampleXBIMConsoleApp
{
    class Program
    {
        static int Main(string[] args)
        {
            var inputIfcFilePath = @"";
            var outputDataJsonDirPath = @"";

            if (args.Length < 4)
            {
                Console.WriteLine("Format for commandline parameters - program.exe -i \"inputString\" -o \"outputString\" ");
                throw new Exception("Format for commandline parameters - program.exe -i \"inputString\" -o \"outputString\" ");
            }

            ParseArgs(args, out inputIfcFilePath, out outputDataJsonDirPath);

            Console.WriteLine("input filepath " + inputIfcFilePath);
            Console.WriteLine("output filepath " + outputDataJsonDirPath);

            var editor = new XbimEditorCredentials
            {
                ApplicationDevelopersName = "You",
                ApplicationFullName = "Your app",
                ApplicationIdentifier = "Your app ID",
                ApplicationVersion = "4.0",
                EditorsFamilyName = "your user",
                EditorsGivenName = "your user",
                EditorsOrganisationName = "Independent Architecture"
            };

            try
            {

                if (!System.IO.File.Exists(outputDataJsonDirPath))
                {
                    System.IO.Directory.CreateDirectory(outputDataJsonDirPath);
                }

                using (var model = IfcStore.Open(inputIfcFilePath, editor))
                {
                    var context = new Xbim3DModelContext(model);

                    // we need to set miminum required precision before creating context.If we dont set this, 
                    // then exception is thrown from context.CreateContext() method due to internal bug in xbim 
                    // link to open issue in XbimGeometry https://github.com/xBimTeam/XbimGeometry/issues/281
                    var sufficientPrecision = 1.0e-5;
                    if (model.ModelFactors.Precision > sufficientPrecision)
                    {
                        model.ModelFactors.Precision = sufficientPrecision;
                    }
                    context.CreateContext(null, false);

                    if (model.GeometryStore.IsEmpty)
                    {
                        throw new Exception(message: "geometry is empty");
                    }
                }

                // if context is getting created correctly write the done file in folder
                var doneFileName = System.IO.Path.Combine(outputDataJsonDirPath, "done.file");
                using (var sw = File.CreateText(doneFileName))
                {
                    sw.WriteLine("done");
                    sw.Close();
                }
            }
            catch (Exception exception)
            {
                //if there is expetion or error write the error file
                var errorFileName = System.IO.Path.Combine(outputDataJsonDirPath, "error.file");

                // Create a new file
                using (var sw = File.CreateText(errorFileName))
                {
                    sw.WriteLine("Exception: Fail to create context.");
                    sw.WriteLine("Message: " + exception.Message);
                    sw.WriteLine("StackTrace: " + exception.StackTrace);
                    sw.Close();
                }
            }

            return 0;
        }

        private static void ParseArgs(string[] args, out string inputIfcFilePath, out string outputDataJsonDirPath)
        {
            inputIfcFilePath = args[0].ToUpper().Equals("-I") ? args[1] : args[3];
            outputDataJsonDirPath = args[0].ToUpper().Equals("-O") ? args[1] : args[3];
        }

    }
}
