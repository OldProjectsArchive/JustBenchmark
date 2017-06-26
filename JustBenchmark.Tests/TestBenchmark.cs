using JustBenchmark.BenchmarkSubscribers;
using Xunit;

namespace JustBenchmark.Tests {
	public class TestBenchmark {
		class BenchmarkClass {
			public int ExecuteCount;

			[Benchmark(100)]
			public void BenchmarkMethod() {
				++ExecuteCount;
			}
		}

		[Fact]
		public void Benchmark() {
			var benchmark = new JustBenchmark(new IBenchmarkSubscribers[0]);
			var obj = new BenchmarkClass();
			benchmark.Run(obj);
			Assert.Equal(obj.ExecuteCount, 100);
			benchmark.Run(obj);
			Assert.Equal(obj.ExecuteCount, 200);
		}
	}
}
