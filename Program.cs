namespace ReadDb;

using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using System.Threading.Tasks;
using Humanizer;
using Spectre.Console;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand();
        rootCommand.Name = "FART";
        rootCommand.Description = "File Association and Rename Tool";

        var renameCommand = new Command("rename")
        {
            new Option<string>("--input", description: "The input file name")
            {
                IsRequired = true
            },
            new Option<string>("--output", description: "The output file name")
            {
                IsRequired = true
            },
        };
        renameCommand.Description = "Rename a file";
        renameCommand.Handler = CommandHandler.Create<string, string>((input, output) =>
        {
            try
            {
                File.Move(input, output);
                AnsiConsole.MarkupLine("[green]Success![/]");
            }
            catch
            {
                AnsiConsole.MarkupLine("[red]An error happened[/]");
            }
        });

        rootCommand.Add(renameCommand);

        var associateCommand = new Command("associate")
        {
            new Option<string>("--extension", description: "The file extension to associate")
            {
                IsRequired = true
            },
            new Option<string>("--program", description: "The absolute path to a program")
            {
                IsRequired = true
            },
        };
        associateCommand.Description = "Associate a file extension to a program";
        associateCommand.Handler = CommandHandler.Create<string, string>((extension, program) =>
        {
            // TODO
        });

        rootCommand.Add(associateCommand);

        AnsiConsole.Render(
            new FigletText("FART")
            .Color(new Color(89, 48, 1)));

        return await rootCommand.InvokeAsync(args);
    }
}
