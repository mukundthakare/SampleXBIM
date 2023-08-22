# SampleXBIMApplication
C# console application to demonstarate, The application developed using XBIM myGet feed does not exit succesfully (exit code is non 0)


Step's to reproduce the issue - 
1) Clone the project.
2) Build it in release mode.
3) To run the application on provided example ifc file use the following command in command line argument.

SampleXBIMConsoleApp.exe -i "D:\E\SampleXBIMData\Schependomlaan.ifc" -o "D:\E\SampleXBIMData\Schependomlaan"

Program arguments - 
-i : input ifc file.
-o : Folder path to put log file.

Sample Image - 
![image](https://github.com/mukundthakare/SampleXBIM/assets/38717053/7f1abc47-4a94-47b2-976b-3b0a6c98e30f)


4) How to check the exit code - 
After execution of the application use following command in same command line.

echo %errorlevel%

Sample Image - 
![image](https://github.com/mukundthakare/SampleXBIM/assets/38717053/a2872e92-babd-4b9e-b28a-c0032d06710b)

As we can see it is non zero exit code.

Sample IFC file can be found in - 
![image](https://github.com/mukundthakare/SampleXBIM/assets/38717053/f1f4bb56-3e8f-4916-9049-287dfaff54b1)
