# JustBenchmark [![Build status](https://ci.appveyor.com/api/projects/status/vgf2oomqw307i44m?svg=true)](https://ci.appveyor.com/project/303248153/justbenchmark) [![NuGet](https://img.shields.io/nuget/vpre/JustBenchmark.svg)](http://www.nuget.org/packages/JustBenchmark)

A very simple benchmark framework works on .Net Framework and .Net Core.

# Example

``` csharp
namespace JustBenchmark.Console {
	using System.Security.Cryptography;
	using System.Text;
	using System.Threading;
	using System.Threading.Tasks;

	class HashBenchmark {
		public byte[] HashExample { get; set; }

		public HashBenchmark() {
			HashExample = Encoding.UTF8.GetBytes("Hello hash benchmark");
		}

		[Benchmark(1000000)]
		[ParallelBenchmark(1000000)]
		public void MD5Benchmark() {
			MD5.Create().ComputeHash(HashExample);
		}

		[Benchmark(1000000)]
		[ParallelBenchmark(1000000)]
		public void SHA1Benchmark() {
			SHA1.Create().ComputeHash(HashExample);
		}

		[Benchmark(20)]
		[ParallelBenchmark(20)]
		public void SleepBenchmark() {
			Thread.Sleep(100);
		}

		[TaskBenchmark(20)]
		[ParallelTaskBenchmark(20)]
		public async Task TaskSleepBenchmark() {
			await Task.Delay(100);
		}
	}

	class Program {
		static void Main(string[] args) {
			var benchmark = new JustBenchmark();
			benchmark.Run(new HashBenchmark());
		}
	}
}
```

# License

MIT License<br/>
Copyright © 2017 303248153@github<br/>
If you have any license issue please contact 303248153@qq.com.<br/>
