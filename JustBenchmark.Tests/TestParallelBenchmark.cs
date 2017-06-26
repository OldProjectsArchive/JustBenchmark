using JustBenchmark.BenchmarkSubscribers;
using System.Threading;
using Xunit;

namespace JustBenchmark.Tests {
	public class TestParallelBenchmark {
		class BenchmarkClass {
			public int ExecuteCount;
			public int ParallelCount;

			[ParallelBenchmark(1000, 8)]
			public void BenchmarkMethod() {
				Interlocked.Increment(ref ParallelCount);
				Interlocked.Increment(ref ExecuteCount);
				Thread.Sleep(1);
				Assert.True(ParallelCount <= 8, "check parallel count");
				Interlocked.Decrement(ref ParallelCount);
			}
		}

		[Fact]
		public void ParallelBenchmark() {
			var benchmark = new JustBenchmark(new IBenchmarkSubscribers[0]);
			var obj = new BenchmarkClass();
			benchmark.Run(obj);
			Assert.Equal(obj.ExecuteCount, 1000);
			benchmark.Run(obj);
			Assert.Equal(obj.ExecuteCount, 2000);
		}
	}
}
