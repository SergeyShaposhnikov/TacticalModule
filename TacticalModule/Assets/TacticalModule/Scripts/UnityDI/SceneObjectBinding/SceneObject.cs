using UnityEngine;

namespace UnityDI.SceneObjectBinding
{
    public class SceneObject : MonoBehaviour
	{
		public static IBuilder Builder { get; set; }
		public bool BuiltUp { get; private set; }

		public virtual void Awake()
		{
			if (!BuiltUp)
			{
				BuiltUp = true;
				Builder.BuildUp(this);
				var initializable = this as IInitializable;
				if (initializable != null)
					initializable.Initialize();
			}
		}

		public void BuildUp()
		{
			if (!BuiltUp)
			{
				BuiltUp = true;
				Builder.BuildUp(this);
			}
		}

	}
}
