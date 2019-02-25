namespace HinxCor.IO
{

    public interface IBitConvertable<T>
    {
        int Length { get; }
        byte[] GetByteArray();
        T ToObject(byte[] array);
        T ToObject();

    }
}
