using System.Xml;

namespace Lead.Detect.Interface.ViewTask
{
    public interface IViewTaskCreator
    {
        /// <summary>
        ///     VisioPrim的类型标识，应具有唯一性
        /// </summary>
        string VisioPrimType { get; }

        /// <summary>
        ///     创建一个空的VisioPrim，用于首次加载
        /// </summary>
        /// <returns></returns>
        IViewTask Create();

        /// <summary>
        ///     根据内容还原一个VisioPrim
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        IViewTask Create(XmlNode config);
    }
}