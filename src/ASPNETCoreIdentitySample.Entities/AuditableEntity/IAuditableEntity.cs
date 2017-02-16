namespace ASPNETCoreIdentitySample.Entities.AuditableEntity
{
    /// <summary>
    /// It's a marker interface, in order to make our entities audit-able.
    /// Every entity you mark with this interface, will save audit info to the database.
    /// More info: http://www.dotnettips.info/post/2577
    /// and http://www.dotnettips.info/post/2578
    /// </summary>
    public interface IAuditableEntity
    {
    }
}