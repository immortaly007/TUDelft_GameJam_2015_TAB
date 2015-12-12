using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface MatrixModifier
{
    void Apply(Transform trans);
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
}
