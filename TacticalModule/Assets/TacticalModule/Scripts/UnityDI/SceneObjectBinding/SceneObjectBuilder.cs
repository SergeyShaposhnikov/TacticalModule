namespace UnityDI.SceneObjectBinding
{
	public class SceneObjectBuilder : Container, IBuilder
	{
		public SceneObjectBuilder()
		{
			SceneObject.Builder = this;
		}

		public void BuildUp(SceneObject sceneObject)
		{
			base.BuildUp(sceneObject);
		}
	}
}
