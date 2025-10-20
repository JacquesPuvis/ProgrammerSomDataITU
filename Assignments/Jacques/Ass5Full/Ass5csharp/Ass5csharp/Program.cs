//5.1 B

class Slay
{
    static int[] merge(int[] xs, int[] ys)
    {
        var myList = xs.Concat(ys);
        return myList.Order().ToArray();
    }


    static void Main(string[] args)
    {
        int[] array1 = { 2, 4, 5 };
        int[] array2 = { 1, 6, 7 };


        int[] SlayList = merge(array1, array2);

        foreach (var item in SlayList)
        {
            Console.Write(item);
        }

    }
}