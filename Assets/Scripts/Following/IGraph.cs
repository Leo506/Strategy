using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Following
{

    public interface IGraph
    {
        List<Vector2> GetNeighnbours();
    }
}