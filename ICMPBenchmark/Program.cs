using ICMPBenchmark;
using System.Net;

int cores = 0;
if (!int.TryParse(string.Join(" ", args), out cores))
	cores = Environment.ProcessorCount;

var threads = new List<Thread>();

for (int i = 0; i < cores; i++)
{
	new Thread(() =>
	{
		var addy = IPAddress.Parse("2602:fa9b:202:1001:001:FF:FF:FF").GetAddressBytes();
        int socket = Ping6Util.socket(10, 3, 58);
        while (true)
		{
            Ping6Util.KernelPing(socket, addy);
        }
        //lol
        Ping6Util.close(socket);
    }).Start();
}

Thread.Sleep(-1);