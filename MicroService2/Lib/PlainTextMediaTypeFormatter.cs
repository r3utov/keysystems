using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CustomFormatters
{
	public class PlainTextMediaTypeFormatter : MediaTypeFormatter
	{
		private Encoding _defaultEncoding;

		public Encoding DefaultEncoding
		{
			get
			{
				if (_defaultEncoding == null) {
					throw new InvalidOperationException($"Для {nameof(PlainTextMediaTypeFormatter)} не задана кодировка");
				}
				return _defaultEncoding;
			}
			set => _defaultEncoding = value;
		}

		public PlainTextMediaTypeFormatter(Encoding defaultEncoding)
		{
			DefaultEncoding = defaultEncoding;
			SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));
		}

		public override bool CanReadType(Type type)
		{
			return type == typeof(string);
		}

		public override bool CanWriteType(Type type)
		{
			return type == typeof(string);
		}

		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			var memoryStream = new MemoryStream();
			readStream.CopyTo(memoryStream);

			var resultString = DefaultEncoding.GetString(memoryStream.ToArray());

			var taskCompletionSource = new TaskCompletionSource<object>();
			taskCompletionSource.SetResult(resultString);
			return taskCompletionSource.Task;
		}

		public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken cancellationToken)
		{
			var bytes = DefaultEncoding.GetBytes(value.ToString());
			return writeStream.WriteAsync(bytes, 0, bytes.Length, cancellationToken);
		}
	}
}