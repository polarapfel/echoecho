using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using CommandLine;
using CommandLine.Text;
using EchoEcho.Common;

namespace EchoEcho
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<EchoOptions>(args)
                .WithParsed<EchoOptions>(opts => RunOptionsAndReturnExitCode(opts))
                .WithNotParsed<EchoOptions>((errs) => HandleParseError(errs));
        }
        
        static int RunOptionsAndReturnExitCode(EchoOptions options)
        {
            EchoClientOptions instanceOptions;

            UriHostNameType hostnameType = Uri.CheckHostName(options.Hostname);
            
            switch (hostnameType)
            {
                case UriHostNameType.Unknown:
                    // throw error
                    break;
                case UriHostNameType.Basic:
                    // treat this as DNS
                case UriHostNameType.Dns:
                    // try to resolve it
                    break;
                case UriHostNameType.IPv4:
                    // Ipv4
                    break;
                default:
                    Console.Error.WriteLine("Unexpected hostname argument error.");
                    Environment.Exit(1);
                    break;
            }
            
            return 0;
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {
            /*foreach (var error in errs)
                Console.Error.WriteLine(error.Tag);*/
        }

    }
}

public class EchoOptions
{
    [Option('b', "bsize", Required = false, HelpText = "Send block of the specified byte size (default: 1024).")]
    public int Blocksize { get; set; }

    [Option('d', "delay", Required = false, HelpText = "Waits for the given amount of microseconds after sending a request before attempting to send the next one. There is no delay by default.")]
    public int Delay { get; set; }
    
    [Option('n', "count", Required = false, HelpText = "Send the specified amount of data blocks for the measurements (default: 100).")]
    public int Blockcount { get; set; }
    
    [Option('f', "fill", Required = false, HelpText = "Read data from the specified file to fill sent blocks with. If the file is smaller than the size of the sum of bytes in blocks to be sent, the remaining trailing bytes are all set to zero. If no file was specified, blocks are filled with random byte values.")]
    public string Filename { get; set; }

    [Option('t', "tcp", Required = false, HelpText = "Requests will be sent over TCP. This is the default.", SetName = "tcp")]
    public bool Tcp { get; set; }
    
    [Option('u', "udp", Required = false, HelpText = "Requests will be sent over UDP.", SetName = "udp")]
    public bool Udp { get; set; }
    
    [Option('V', "version", Required = false, HelpText = "Display program version and license and exit.")]
    public String Version { get; set; }

    [Value(0, Required = true, MetaName = "hostname", HelpText = "hostname")]
    public string Hostname { get; set; }
    
    [Value(1, Required = false, MetaName = "port", HelpText = "port", Default = 7)]
    public int Port { get; set; }
}
