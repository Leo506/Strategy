using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Following
{

    public interface IGraph
    {
        List<IGraph> GetNeighnbours();

        Vector2 GetPosition();
    }
}