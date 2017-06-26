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
