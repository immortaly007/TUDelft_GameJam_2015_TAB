class Tuple<T1, T2>
{
    public T1 Value1 { get; set; }
    public T2 Value2 { get; set; }

    public Tuple() { }

    public Tuple(T1 value1, T2 value2)
    {
        Value1 = value1;
        Value2 = value2;
    }
}