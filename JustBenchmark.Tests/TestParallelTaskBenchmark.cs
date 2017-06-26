using JustBenchmark.BenchmarkSubscribers;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JustBenchmark.Tests {
	public class TestParallelTaskBenchmark {
		class BenchmarkClass {
			public int ExecuteCount;
			public int ParallelCount;

			[ParallelTaskBenchmark(1000, 8)]
			public async Task BenchmarkMethod() {
				Interlocked.Increment(ref ParallelCount);
				Interlocked.Increment(ref ExecuteCount);
				await Task.Delay(1);
				Assert.True(ParallelCount <= 8, "check parallel count");
				Interlocked.Decrement(ref ParallelCount);
			}
		}

		[Fact]
		public void ParallelBenchmark() {
			var benchmark = new JustBenchmark(new IBenchmarkSubscribers[0]);
			var obj = new BenchmarkClass();
			benchmark.Run(obj);
			Assert.Equal(1000, obj.ExecuteCount);
			benchmark.Run(obj);
			Assert.Equal(2000, obj.ExecuteCount);
		}
	}
}
