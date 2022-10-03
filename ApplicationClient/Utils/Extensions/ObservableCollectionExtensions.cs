using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace ApplicationClient.Utils.Extensions;
public static class ObservableCollectionExtensions 
{
	public static ObservableCollection<T> AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> enumerable) 
	{
		ArgumentNullException.ThrowIfNull(collection);
		ArgumentNullException.ThrowIfNull(enumerable);

		foreach(var item in enumerable)
			collection.Add(item);

		return collection;
	}
}