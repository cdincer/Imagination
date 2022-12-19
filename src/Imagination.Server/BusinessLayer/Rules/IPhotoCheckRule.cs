namespace Imagination.BusinessLayer.Rules
{
    public interface IPhotoCheckRule
    {
        bool CheckPhotoRule(byte[] FilePiece,int FileSize );
    }
}
