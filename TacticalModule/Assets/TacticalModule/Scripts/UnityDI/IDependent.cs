namespace UnityDI
{
	public interface IDependent
	{
		/// <summary>
		/// Вызывается после того, как все зависимости были проставлены
		/// </summary>
		void OnInjected();
	}
}
