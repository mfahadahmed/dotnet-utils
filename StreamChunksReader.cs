using System.Collections.Generic;
using System.IO;

public static class StreamChunksReader
{
	/// <summary>
	/// Return rows in chunks upto the specified row limit.
	/// </summary>
	/// <param name="stream">Stream instance to read the rows from</param>
	/// <param name="rowLimit">Number of rows to read per iteration</param>
	/// <returns>List<string> | List of rows read in one iteration</returns>
	public static IEnumerable<List<string>> ReadRows(Stream stream, uint rowLimit = 10)
	{
		int rowCount = 0;
		var streamRows = new List<string>();

		using (var sr = new StreamReader(stream))
		{
			string row;
			while ((row = sr.ReadLine()) != null)
			{
				streamRows.Add(row);
				++rowCount;

				if (rowCount == rowLimit)
				{
					rowCount = 0;
					yield return streamRows;
					streamRows = new List<string>();
				}
			}
		}

		if (streamRows.Count > 0)
			yield return streamRows;
	}
}
