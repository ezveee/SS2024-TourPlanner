using Business;

namespace UI.Models;

internal class ImageModel
{
	public string img;

	public ImageModel()
	{
		_ = new ImageLoader();
	}
}
