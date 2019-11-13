using System;
using System.Collections.Generic;

public class Deck<T> : List<T>
{
	private Random r_instance;
	public int index { get; private set; } = 0;

	#region Constructors
	public Deck() : base()
	{
		r_instance = new Random((int)DateTime.Now.Ticks);
	}
	public Deck(int capacity) : base(capacity)
	{
		r_instance = new Random((int)DateTime.Now.Ticks);
	}
	public Deck(IEnumerable<T> collection) : base(collection)
	{
		r_instance = new Random((int)DateTime.Now.Ticks);
	}

	public Deck(int capacity, int seed = 0) : base(capacity)
	{
		if (seed == 0)
		{
			r_instance = new Random((int)DateTime.Now.Ticks);
		}
		else
		{
			r_instance = new Random(seed);
		}
	}
	//int capacity
	//IEnumerable<T> collection
	#endregion

	#region Public Methods

	public void Seed(int seed)
	{
		r_instance = new Random(seed);
	}

	/// <summary>
	/// Randomly assigns a position to each card in the deck.
	/// </summary>
	public void Shuffle()
	{
		for (int i = 0; i < this.Count; i++)
		{
			int randomInt = r_instance.Next(this.Count);
			Swap(i, randomInt);
		}
	}

	/// <summary>
	/// Shuffles all cards in the deck after the current card, such that no cards already drawn will be shuffled.
	/// </summary>
	public void ShuffleRemaining()
	{
		for (int i = index; i < this.Count; i++)
		{
			int randomInt = r_instance.Next(index, this.Count);
			Swap(i, randomInt);
		}
		ShuffleRange(0, 1);
	}

	/// <summary>
	/// Shuffles all cards in the deck between minRange (inclusive) and maxRange (exclusive).
	/// </summary>
	public void ShuffleRange(int minRange, int maxRange)
	{
		int min = minRange < 0 ? 0 : minRange;
		int max = maxRange > this.Count ? this.Count : maxRange;

		for (int i = min; i < max; i++)
		{
			int randomInt = r_instance.Next(min, max);
			Swap(i, randomInt);
		}
	}

	public T Draw()
	{
		T ret = this[index++];
		if (index >= this.Count)
		{
			index = 0;
			Shuffle();
		}
		return ret;
	}

	public void Add(T item, bool shuffle = true)
	{
		base.Add(item);
		if (shuffle)
		{
			Shuffle();
		}
	}

	#region Removal Functions
	public bool Remove(T item, bool shuffle = true)
	{
		bool ret = base.Remove(item);
		if (shuffle)
		{
			Shuffle();
		}
		return ret;
	}
	public int RemoveAll(Predicate<T> match, bool shuffle = true)
	{
		int ret = base.RemoveAll(match);
		if (shuffle)
		{
			Shuffle();
		}
		return ret;
	}
	public void RemoveAt(int index, bool shuffle = true)
	{
		base.RemoveAt(index);
		if (shuffle)
		{
			Shuffle();
		}
	}
	public void RemoveRange(int index, int count, bool shuffle = true)
	{
		base.RemoveRange(index, count);
		if (shuffle)
		{
			Shuffle();
		}
	}
	#endregion

	#endregion

	#region Private Methods
	private void Swap(int index1, int index2)
	{
		T thing = this[index1];
		this[index1] = this[index2];
		this[index2] = thing;
	}
	#endregion

}

