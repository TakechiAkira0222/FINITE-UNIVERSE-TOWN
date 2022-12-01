using UnityEngine;
using System;
using TakechiEngine.PUN.ServerAccess;

namespace Takechi.ServerAccess.Singleton
{
    public abstract class SingletonTakechiServerAccessPunCallbacks<T> : TakechiServerAccessPunCallbacks where T : TakechiServerAccessPunCallbacks
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    Type t = typeof(T);

                    instance = (T)FindObjectOfType(t);
                    if (instance == null)
                    {
                        Debug.LogError(t + " ���A�^�b�`���Ă���GameObject�͂���܂���");
                    }
                }

                return instance;
            }
        }

        virtual protected void Awake()
        {
            // ���̃Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă��邩���ׂ�
            // �A�^�b�`����Ă���ꍇ�͔j������B
            CheckInstance();
        }

        protected bool CheckInstance()
        {
            if (instance == null)
            {
                instance = this as T;
                return true;
            }
            else if (Instance == this)
            {
                return true;
            }
            Destroy(this);
            return false;
        }
    }
}