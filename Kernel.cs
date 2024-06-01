using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Sys = Cosmos.System;

namespace MarkOS
{
    public class Kernel : Sys.Kernel
    {
        Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
        bool InitFS = true;

        protected override void BeforeRun()
        {
            Console.WriteLine("MarkOS is now running.");
        }

        protected override void Run()
        {
            if (InitFS)
            {
                Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
                InitFS = false;
            }

            Console.Write("Minter> ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "help":
                    try
                    {
                        Console.WriteLine(File.ReadAllText(@"0:\help.txt"));
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Help file not found, disk format required");
                    }
                    break;

                case "echo":
                    Console.Write("Echo what? ");
                    var echoInput = Console.ReadLine();
                    Console.WriteLine(echoInput);
                    break;

                case "dir":
                    var filesList = Directory.GetFiles(@"0:\");
                    var directoryList = Directory.GetDirectories(@"0:\");
                    foreach (var file in filesList)
                    {
                        Console.WriteLine(file);
                    }
                    foreach (var directory in directoryList)
                    {
                        Console.WriteLine(directory);
                    }
                    break;

                case "cls":
                    Console.Clear();
                    break;

                case "medit":
                    Console.WriteLine("WIP feature");

                    Console.Clear();
                    Console.WriteLine("Mark Edit (Medit)");
                    Console.Write("Filename? ");
                    var filename = Console.ReadLine();
                    Console.Write("edit or create?");
                    var choice = Console.ReadLine();
                    if (choice == "Edit")
                    {
                        Console.WriteLine("x by itself to exit and newlines are automatic, just press enter for newline");
                        Console.WriteLine("Will add to end of file, type d by itself to clean file");
                        var text = Console.ReadLine();

                    }
                    break;

                case "format":
                    for (int i = 0; i < fs.Disks[0].Partitions.Count; i++)
                    {
                        fs.Disks[0].DeletePartition(i);
                    }
                    fs.Disks[0].Clear();
                    fs.Disks[0].CreatePartition(128);
                    fs.Disks[0].FormatPartition(0, "FAT32", true);
                    break;

                case "exit":
                    Console.Clear();
                    while (0 == 0)
                    {
                        Console.WriteLine("It is now safe to shutdown.");
                    }
                    break;

                case "reboot":
                    Console.Clear();
                    Console.WriteLine("The system will reboot now!");
                    Cosmos.System.Power.Reboot();
                    break;

                default:
                    Console.WriteLine("Invalid command or syntax");
                    break;
            }
        }
    }
}
