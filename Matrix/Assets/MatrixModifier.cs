using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface MatrixModifier
{
	MatrixModifier Clone();
    void Apply(Transform trans);
	void Tween(float ratio);
	string getText();
}

class RotationMatrixModifier : MatrixModifier
{
    float angle;

    public RotationMatrixModifier(float angle)
    {
        this.angle = angle;
    }

    public void Apply(Transform trans)
    {
        trans.Rotate(0, 0, angle);

    }

	public void Tween(float ratio)
	{
		angle = angle * ratio;
	}

	public MatrixModifier Clone() {
		return new RotationMatrixModifier (angle);
	}

	public string getText() {
		return "Rotate (" + angle + ")";
	}

}

class TranslationMatrixModifier : MatrixModifier
{
    Vector2 translation;

    public TranslationMatrixModifier(Vector2 translation)
    {
        this.translation = translation;
    }

    public void Apply(Transform trans)
    {
        trans.localPosition = new Vector3(trans.localPosition.x + translation.x, trans.localPosition.y * translation.y, trans.localPosition.z);
    }

	public void Tween(float ratio) {
		//this.translation = this.translation * ratio;

		translation.Set (translation.x * ratio, translation.y * ratio);

	}

	public MatrixModifier Clone() {
		return new TranslationMatrixModifier (translation);
	}

	public string getText() {
		return "Translate (" + translation.x + "," + translation.y + ")";
	}
}

class ScalingMatrixModifier : MatrixModifier
{
    Vector2 scale;

    public ScalingMatrixModifier(Vector2 scale)
    {
        this.scale = scale;
    }

    public void Apply(Transform trans)
    {
        trans.localScale = new Vector3(trans.localScale.x * scale.x, trans.localScale.y * scale.y, trans.localScale.z);
    }

	public void Tween(float ratio) {
		Vector2 originalScale = new Vector2();
		originalScale.Set (1.0f, 1.0f);

		scale.Set (scale.x * ratio + (1 - ratio) * 1, scale.y * ratio + (1-ratio) * 1);

		//this.scale = this.scale.Scale(ratio) + originalScale.Scale(1-ratio);
	}

	public MatrixModifier Clone() {
		return new ScalingMatrixModifier (scale);
	}

	public string getText() {
		return "Scale (" + scale.x + "," + scale.y + ")";
	}
}
