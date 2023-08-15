using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Domin.Enitiy
{
    public class HoleManager
    {
        private static HoleManager _instance;

        private List<Hole> _holeList;
        private List<Hole> _templeHolePositionList;

        private HoleManager()
        {
            _holeList = GenerateHoles();
            _templeHolePositionList = new List<Hole>();
        }

        public static HoleManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new HoleManager();

                return _instance;
            }
        }


        private List<Hole> GenerateHoles()
        {
            var parent = GameObject.Find("Holes");

            if (parent is null)
            {
                return new List<Hole> { };
            }
            else
            {
                var holeImages = new List<Transform> { };
                var holes = new List<Hole>();

                var list = parent.transform.Cast<Transform>()
                    .Aggregate(holeImages, (current, child) => current.Append(child).ToList());

                for (int i = 0; i < list.Count; i++)
                {
                    holes.Add(new Hole(i, list[i]));
                }

                return holes;
            }
        }

        public Hole GetRandomHole()
        {
            int index = Random.Range(0, _holeList.Count);

            var currentHole = _holeList[index];

            _holeList.Remove(currentHole);

            _templeHolePositionList.Add(currentHole);

            return currentHole;
        }

        public void RecycleHole(Hole hole)
        {
            _holeList.AddRange(_templeHolePositionList.Where(holePosition => holePosition == hole).ToList());
            _templeHolePositionList.Remove(hole);
        }
    }
}