using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Poe_show_buff
{
    public class Settings
    {
        // line - buff line, where frist only buffs, second debuff (but if we have to many buffs then 1-2 = buffs and 3 debuff)
        // в другом месте это запускаю - там где таймер, эта штука должна принимать штуки "в какие штуках смотреть(1 строка, вторая и етк), как глубоко"
        // получить позицию линии
        // бафы или дебафы показываются в лнии, во второй линиии 90% дебафов, но если бафов слишком много, то 3 линия становится дебафом

        private int _howManyLineNeedToWatch;
        private int _distanceBetweenLines = 10;
        private Vector2 _firstLinesPosition = new Vector2(8, 10);
        private bool _haveSummons;

        private bool isBuff, isDeBuff, isTrade;
        public void UpdateSettings(int howManyLines)
        {
            if (_haveSummons)
            {
                // меняем лефолтное положение первой строки
            }
            _howManyLineNeedToWatch = howManyLines;
        }

        // пока похуй окей
       
    }
}
