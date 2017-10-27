using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Interfaces;
using NaturalNumbersMath;

namespace Game
{
    public class ObservableBlueprintBuilder : IObservableBlueprintBuilder
    {
        private IBlueprintBuilder baseBuilder;
        private List<IBlueprintBuilderObserver> observers;

        public int Height
        {
            get
            {
                return baseBuilder.Height;
            }
        }

        public int Width
        {
            get
            {
                return baseBuilder.Width;
            }
        }

        public ObservableBlueprintBuilder(IBlueprintBuilder baseBuilder)
        {
            this.baseBuilder = baseBuilder;
            observers = new List<IBlueprintBuilderObserver>();
        }

        #region Block Creation

        public bool CreateBlock(Coordinate position)
        {
            var success = baseBuilder.CreateBlock(position);
            if (success)
            {
                NotifyObserverOfBlockCreated(position);
            }
            else
            {
                NotifyObserverOfErrorWhenCreatingBlock(position);
            }

            return success;
        }

        private void NotifyObserverOfBlockCreated(Coordinate position)
        {
            foreach (var observer in observers)
            {
                observer.BlockCreated(this, position);
            }
        }

        private void NotifyObserverOfErrorWhenCreatingBlock(Coordinate position)
        {
            foreach (var observer in observers)
            {
                observer.ErrorBlockNotCreated(this, position);
            }
        }

        #endregion

        #region Block Deletion

        public bool DeleteBlock(Coordinate position)
        {
            var success = baseBuilder.DeleteBlock(position);
            if (success)
            {
                NotifyObserverOfBlockDeleted(position);
            }
            else
            {
                NotifyObserverOfErrorWhenDeletingBlock(position);
            }

            return success;
        }

        private void NotifyObserverOfErrorWhenDeletingBlock(Coordinate position)
        {
            foreach (var observer in observers)
            {
                observer.ErrorBlockNotDeleted(this, position);
            }
        }

        private void NotifyObserverOfBlockDeleted(Coordinate position)
        {
            foreach (var observer in observers)
            {
                observer.BlockDeleted(this, position);
            }
        }
        #endregion

        #region Ship Component Adding

        public bool AddShipComponent(Coordinate position, IShipComponent shipComponent)
        {
            var success = baseBuilder.AddShipComponent(position, shipComponent);

            if (success)
            {
                NotifyObserverOfShipComponentAdded(position);
            }
            else
            {
                NotifyObserverOfErrorWhenAddingShipComponent(position);
            }

            return success;
        }

        private void NotifyObserverOfShipComponentAdded(Coordinate position)
        {
            foreach (var observer in observers)
            {
                observer.ShipComponentAdded(this, position);
            }
        }

        private void NotifyObserverOfErrorWhenAddingShipComponent(Coordinate position)
        {
            foreach (var observer in observers)
            {
                observer.ErrorShipComponentNotAdded(this, position);
            }
        }
        #endregion

        #region Ship Component Deletion

        public bool DeleteShipComponent(Coordinate position)
        {
            var success = baseBuilder.DeleteShipComponent(position);

            if (success)
            {
                NotifyObserverOfShipComponentDeleted(position);
            }
            else
            {
                NotifyObserverOfErrorWhenDeletingShipComponent(position);
            }

            return true;
        }

        private void NotifyObserverOfShipComponentDeleted(Coordinate position)
        {
            foreach (var observer in observers)
            {
                observer.ShipComponentDeleted(this, position);
            }
        }

        private void NotifyObserverOfErrorWhenDeletingShipComponent(Coordinate position)
        {
            foreach (var observer in observers)
            {
                observer.ErrorShipComponentNotDeleted(this, position);
            }
        }
        #endregion

        public void AttachObserver(IBlueprintBuilderObserver observer)
        {
            observers.Add(observer);
        }

        public IBlock GetBlock(Coordinate position)
        {
            return baseBuilder.GetBlock(position);
        }    
    }
}
