using JustBenchmark.BenchmarkExecutors;
using JustBenchmark.BenchmarkSubscribers;
using System.Collections.Generic;
using System.Reflection;

namespace JustBenchmark {
	/// <summary>
	/// Benchmark main class
	/// </summary>
	public class JustBenchmark {
		/// <summary>
		/// Subscribers, maybe empty
		/// </summary>
		private IList<IBenchmarkSubscribers> _subscribers { get; set; }

		/// <summary>
		/// Initialize
		/// </summary>
		public JustBenchmark() : this(new[] { new ConsoleBenchmarkSubscriber() }) {

		}

		/// <summary>
		/// Initialize
		/// </summary>
		public JustBenchmark(IEnumerable<IBenchmarkSubscribers> subscribers) {
			_subscribers = new List<IBenchmarkSubscribers>(subscribers);
		}

		/// <summary>
		/// Run benchmark methods in given instance
		/// </summary>
		/// <param name="benchmarkInstance">Instance contains benchmark methods</param>
		public void Run(object benchmarkInstance) {
			var type = benchmarkInstance.GetType();
			foreach (var method in type.GetTypeInfo().GetMethods()) {
				var attributes = method.GetCustomAttributes();
				foreach (var attribute in attributes) {
					var executor = attribute as IBenchmarkExecutor;
					if (executor == null) {
						continue;
					}
					var result = executor.Execute(method, benchmarkInstance);
					foreach (var subscriber in _subscribers) {
						subscriber.OnResult(result);
					}
				}
			}
		}
	}
}
