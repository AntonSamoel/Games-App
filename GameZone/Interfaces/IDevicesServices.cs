namespace GameZone.Base
{
    public interface IDevicesServices : IMainService<Device>
    {
        public IEnumerable<SelectListItem> GetDevices();
    }
}
