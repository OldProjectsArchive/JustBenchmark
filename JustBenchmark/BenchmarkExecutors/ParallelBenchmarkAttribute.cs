using JustBenchmark.BenchmarkExecutors;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace JustBenchmark {
	/// <summary>
	/// Multi thread benchmark for blocking method
	/// </summary>
	public class ParallelBenchmarkAttribute : Attribute, IBenchmarkExecutor {
		/// <summary>
		/// Iteration count
		/// </summary>
		public int Iteration { get; set; } = 10000;
		/// <summary>
		/// Thread count
		/// </summary>
		public int Threads { get; set; } = Environment.ProcessorCount;

		/// <summary>
		/// Initialize
		/// </summary>
		public ParallelBenchmarkAttribute() {

		}

		/// <summary>
		/// Initialize
		/// </summary>
		public ParallelBenchmarkAttribute(int iteration) {
			Iteration = iteration;
		}

		/// <summary>
		/// Initialize
		/// </summary>
		public ParallelBenchmarkAttribute(int iteration, int threads) {
			Iteration = iteration;
			Threads = threads;
		}

		/// <summary>
		/// Execute benchmark
		/// </summary>
		public BenchmarkResult Execute(MethodInfo method, object instance) {
			var action = (Action)method.CreateDelegate(typeof(Action), instance);
			var threads = new List<Thread>();
			var lastExecption = (Exception)null;
			var count = 0;
			for (int from = 0, to = Threads; from < to; ++from) {
				threads.Add(new Thread(() => {
					try {
						while (true) {
							var id = Interlocked.Increment(ref count) - 1;
							if (id >= Iteration) {
								break;
							}
							action();
						}
					} catch (Exception ex) {
						lastExecption = ex;
					}
				}));
			}
			var result = this.GenericExecute(method, () => {
				foreach (var thread in threads) {
					thread.Start();
				}
				foreach (var thread in threads) {
					thread.Join();
				}
			});
			if (lastExecption != null) {
				throw lastExecption;
			}
			return result;
		}
	}
}
