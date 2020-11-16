using System.Collections;

public class ShuffleArray 
{
    public static T[] ShuffleThisArray<T>(T[] _array)
    {
        System.Random random = new System.Random();

        int length = _array.Length - 1;
        for (int i = 0; i < length; i++)
        {
            int r_Index = i + random.Next(length - i);
            T temporaryItem = _array[r_Index];
            _array[r_Index] = _array[i];
            _array[i] = temporaryItem;
        }
        return _array;
    }
    
}
