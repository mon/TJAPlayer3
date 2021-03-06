﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using SlimDX;
using FDK;

namespace DTXMania
{
	internal class CAct演奏Combo共通 : CActivity
	{
		// プロパティ

		public STCOMBO n現在のコンボ数;
		public struct STCOMBO
		{
			public CAct演奏Combo共通 act;

			public int this[ int index ]
			{
				get
				{
					switch( index )
					{
						case 0:
							return this.P1;

						case 1:
							return this.P2;

						case 2:
							return this.P3;

                        case 3:
                            return this.P4;
					}
					throw new IndexOutOfRangeException();
				}
				set
				{
					switch( index )
					{
						case 0:
							this.P1 = value;
							return;

						case 1:
							this.P2 = value;
							return;

						case 2:
							this.P3 = value;
							return;

						case 3:
							this.P4 = value;
							return;
					}
					throw new IndexOutOfRangeException();
				}
			}
			public int P1
			{
				get
				{
					return this.p1;
				}
				set
				{
					this.p1 = value;
					if( this.p1 > this.P1最高値 )
					{
						this.P1最高値 = this.p1;
					}
					this.act.status.P1.nCOMBO値 = this.p1;
					this.act.status.P1.n最高COMBO値 = this.P1最高値;
				}
			}
			public int P2
			{
				get
				{
					return this.p2;
				}
				set
				{
					this.p2 = value;
					if( this.p2 > this.P2最高値 )
					{
						this.P2最高値 = this.p2;
					}
					this.act.status.P2.nCOMBO値 = this.p2;
					this.act.status.P2.n最高COMBO値 = this.P2最高値;
				}
			}
			public int P3
			{
				get
				{
					return this.p3;
				}
				set
				{
					this.p3 = value;
					if( this.p3 > this.P3最高値 )
					{
						this.P3最高値 = this.p3;
					}
					this.act.status.P3.nCOMBO値 = this.p3;
					this.act.status.P3.n最高COMBO値 = this.P3最高値;
				}
			}
			public int P4
			{
				get
				{
					return this.p4;
				}
				set
				{
					this.p4 = value;
					if( this.p4 > this.P4最高値 )
					{
						this.P4最高値 = this.p4;
					}
					this.act.status.P4.nCOMBO値 = this.p4;
					this.act.status.P4.n最高COMBO値 = this.P4最高値;
				}
			}
			public int P1最高値 { get; private set; }
			public int P2最高値 { get; private set; }
			public int P3最高値 { get; private set; }
			public int P4最高値 { get; private set; }

			private int p1;
			private int p2;
			private int p3;
			private int p4;
		}
		public C演奏判定ライン座標共通 演奏判定ライン座標
		{
			get;
			set;
		}

		protected enum EEvent { 非表示, 数値更新, 同一数値, ミス通知 }
		protected enum EMode { 非表示中, 進行表示中, 残像表示中 }
		protected const int nドラムコンボのCOMBO文字の高さ = 32;
		protected const int nドラムコンボのCOMBO文字の幅 = 90;
		protected const int nドラムコンボの高さ = 115;
		protected const int nドラムコンボの幅 = 90;
		protected const int nドラムコンボの文字間隔 = -6;
		protected int[] nジャンプ差分値 = new int[ 180 ];
		protected CSTATUS status;
        //protected CTexture txCOMBO太鼓;
        //protected CTexture txCOMBO太鼓_でかいやつ;
        //protected CTexture txコンボラメ;
        public CCounter[] ctコンボ加算;
        public CCounter ctコンボラメ;

        protected float[,] nコンボ拡大率_座標 = new float[,]{
                        {1.11f,-7},
                        {1.22f,-14},
                        {1.2f,-12},
                        {1.15f,-9},
                        {1.13f,-8},
                        {1.11f,-7},
                        {1.06f,-3},
                        {1.04f,-2},
                        {1.0f,0},
                    };
        protected float[,] nコンボ拡大率_座標_100combo = new float[,]{
                        {0.81f,-7},
                        {0.92f,-14},
                        {0.9f,-12},
                        {0.85f,-9},
                        {0.83f,-8},
                        {0.81f,-7},
                        {0.78f,-3},
                        {0.74f,-2},
                        {0.7f,0},
                };
    protected float[,] nコンボ拡大率_座標_1000combo = new float[,]{
                        {1.11f,-7},
                        {1.22f,-14},
                        {1.2f,-12},
                        {1.15f,-9},
                        {1.13f,-8},
                        {1.11f,-7},
                        {1.06f,-3},
                        {1.04f,-2},
                        {1.0f,0},
                    };

        // 内部クラス

        protected class CSTATUS
		{
			public CSTAT P1 = new CSTAT();
			public CSTAT P2 = new CSTAT();
			public CSTAT P3 = new CSTAT();
			public CSTAT P4 = new CSTAT();
			public CSTAT this[ int index ]
			{
				get
				{
					switch( index )
					{
						case 0:
							return this.P1;

						case 1:
                            return this.P2;

						case 2:
							return this.P3;

						case 3:
							return this.P4;
					}
					throw new IndexOutOfRangeException();
				}
				set
				{
					switch( index )
					{
						case 0:
							this.P1 = value;
							return;

						case 1:
							this.P2 = value;
							return;

						case 2:
							this.P3 = value;
							return;

						case 3:
							this.P4 = value;
							return;
					}
					throw new IndexOutOfRangeException();
				}
			}

			public class CSTAT
			{
				public CAct演奏Combo共通.EMode e現在のモード;
				public int nCOMBO値;
				public long nコンボが切れた時刻;
				public int nジャンプインデックス値;
				public int n現在表示中のCOMBO値;
				public int n最高COMBO値;
				public int n残像表示中のCOMBO値;
				public long n前回の時刻_ジャンプ用;
			}
		}


		// コンストラクタ

		public CAct演奏Combo共通()
		{
			this.b活性化してない = true;

			// 180度分のジャンプY座標差分を取得。(0度: 0 → 90度:-15 → 180度: 0)
			for( int i = 0; i < 180; i++ )
				this.nジャンプ差分値[ i ] = (int) ( -15.0 * Math.Sin( ( Math.PI * i ) / 180.0 ) );
			演奏判定ライン座標 = new C演奏判定ライン座標共通();
		}


		// メソッド

		protected virtual void tコンボ表示_ドラム( int nCombo値, int nジャンプインデックス )
		{
		}

      	protected virtual void tコンボ表示_太鼓( int nCombo値, int nジャンプインデックス, int nPlayer )
		{
            //テスト用コンボ数
            //nCombo値 = 114;
			#region [ 事前チェック。]
			//-----------------
			if( CDTXMania.ConfigIni.bドラムコンボ表示 == false )
				return;		// 表示OFF。

			if( nCombo値 == 0 )
				return;		// コンボゼロは表示しない。
			//-----------------
			#endregion

			int[] n位の数 = new int[ 10 ];	// 表示は10桁もあれば足りるだろう

            this.ctコンボラメ.t進行Loop();
            this.ctコンボ加算[ nPlayer ].t進行();

			#region [ nCombo値を桁数ごとに n位の数[] に格納する。（例：nCombo値=125 のとき n位の数 = { 5,2,1,0,0,0,0,0,0,0 } ） ]
			//-----------------
			int n = nCombo値;
			int n桁数 = 0;
			while( ( n > 0 ) && ( n桁数 < 10 ) )
			{
				n位の数[ n桁数 ] = n % 10;		// 1の位を格納
				n = ( n - ( n % 10 ) ) / 10;	// 右へシフト（例: 12345 → 1234 ）
				n桁数++;
			}
			//-----------------
			#endregion

			#region [ n位の数[] を、"COMBO" → 1の位 → 10の位 … の順に、右から左へ向かって順番に表示する。]
			//-----------------
			const int n1桁ごとのジャンプの遅れ = 30;	// 1桁につき 50 インデックス遅れる


            //X右座標を元にして、右座標 - ( コンボの幅 * 桁数 ) でX座標を求めていく?

			int nY上辺位置px = CDTXMania.ConfigIni.bReverse.Drums ? 350 : 10;
			int n数字とCOMBOを合わせた画像の全長px = ( ( 44 ) * n桁数 );
			int x = 245 + ( n数字とCOMBOを合わせた画像の全長px / 2 );
			//int y = 212;
            int y = CDTXMania.Skin.nComboNumberY[ nPlayer ];

            #region[ コンボ文字 ]
            if( n桁数 <= 2 )
            {
                if( CDTXMania.Tx.Taiko_Combo[0] != null )
                {
                    CDTXMania.Tx.Taiko_Combo[0].vc拡大縮小倍率.Y = 0.8f;
                    CDTXMania.Tx.Taiko_Combo[0].vc拡大縮小倍率.X = 0.8f;
                    CDTXMania.Tx.Taiko_Combo[0].t2D描画(CDTXMania.app.Device, 240, CDTXMania.Skin.nComboNumberTextY[nPlayer] + 1, new Rectangle(0, 60, 70, 34));
                }
            }
            else
            {
                if(CDTXMania.Tx.Taiko_Combo[1] != null )
                {
                    CDTXMania.Tx.Taiko_Combo[1].vc拡大縮小倍率.Y = 0.8f;
                    CDTXMania.Tx.Taiko_Combo[1].vc拡大縮小倍率.X = 0.8f;
                    CDTXMania.Tx.Taiko_Combo[1].t2D描画( CDTXMania.app.Device, 242, CDTXMania.Skin.nComboNumberTextLargeY[ nPlayer ], new Rectangle( 0, 70, 70, 34 ) );
                }
            }
            #endregion

            for ( int i = 0; i < n桁数; i++ )
            {

                this.nコンボ拡大率_座標 = new float[,]{
                        {0.82f,-1},
                        {0.86f,-3},
                        {0.92f,-6},
                        {0.96f,-8},
                        {0.98f,-10},
                        {0.98f,-10},
                        {0.96f,-8},
                        {0.92f,-6},
                        {0.86f,-3},
                        {0.84f,-3},
                        {0.83f,-2},
                        {0.82f,-2},
                        {0.81f,-1},
                        {0.8f,0},
                };
                this.nコンボ拡大率_座標_100combo = new float[,]{
                        {0.72f,-2}, //.2
                        {0.76f,-5}, //.4
                        {0.82f,-10}, //.6
                        {0.86f,-12}, //.4
                        {0.88f,-14}, //.2
                        {0.88f,-14}, //0
                        {0.86f,-12}, //-.2
                        {0.82f,-10}, //-.4
                        {0.76f,-5}, //-.6
                        {0.74f,-4}, //-.4
                        {0.73f,-3}, //-.1
                        {0.72f,-2}, //-1
                        {0.71f,-1},
                        {0.7f,0}
                };
                this.nコンボ拡大率_座標_1000combo = new float[,]{
                        {0.62f,-2}, //.2
                        {0.66f,-5}, //.4
                        {0.72f,-10}, //.6
                        {0.76f,-12}, //.4
                        {0.78f,-14}, //.2
                        {0.78f,-14}, //0
                        {0.76f,-12}, //-.2
                        {0.72f,-10}, //-.4
                        {0.66f,-5}, //-.6
                        {0.64f,-4}, //-.4
                        {0.63f,-3}, //-.1
                        {0.62f,-2}, //-1
                        {0.61f,-1},
                        {0.6f,0}
                };

                if ( n桁数 <= 1 )
                {
                    int nCombo中心X = 271; //仮置き
                    
                    int nTex横幅 = 44;
                    int nComboPadding = -2;
                    int[] arComboX = { nCombo中心X, nCombo中心X };
				    if(CDTXMania.Tx.Taiko_Combo[0] != null )
				    {
                        CDTXMania.Tx.Taiko_Combo[0].vc拡大縮小倍率.Y = this.nコンボ拡大率_座標[ this.ctコンボ加算[ nPlayer ].n現在の値, 0 ];
                        CDTXMania.Tx.Taiko_Combo[0].t2D中心基準描画( CDTXMania.app.Device, arComboX[ i ], y + (int)this.nコンボ拡大率_座標[ this.ctコンボ加算[ nPlayer ].n現在の値, 1 ] + 44, new Rectangle( n位の数[ i ] * 44, 0, 44, 60 ) );
				    }
                }
                else if( n桁数 <= 2 )
                {
                    int nCombo中心X = 271; //仮置き
                    int nTex横幅 = 44;
                    int nComboPadding = -2;
                    int[] arComboX = { nCombo中心X + ( 16 - 1 ), nCombo中心X - ( 16 - 1 ) };
				    if(CDTXMania.Tx.Taiko_Combo[0] != null )
				    {
                        CDTXMania.Tx.Taiko_Combo[0].vc拡大縮小倍率.Y = this.nコンボ拡大率_座標[ this.ctコンボ加算[ nPlayer ].n現在の値, 0 ];
                        CDTXMania.Tx.Taiko_Combo[0].t2D中心基準描画( CDTXMania.app.Device, arComboX[ i ], y + (int)this.nコンボ拡大率_座標[ this.ctコンボ加算[ nPlayer ].n現在の値, 1 ] + 44, new Rectangle( n位の数[ i ] * 44, 0, 44, 60 ) );
				    }
                }
                else if( n桁数 == 3 )
                {
                    x -= 31;
                    //int nラメ基準Y座標 = 199; //2列目のラメの始点を基準とする。
                    int nラメ基準Y座標 = CDTXMania.Skin.nComboNumberY[ nPlayer ] - 4; //2列目のラメの始点を基準とする。
                    int nラメ基準X座標 = x + ( 16 - 9 );
				    if(CDTXMania.Tx.Taiko_Combo[1] != null )
				    {
                        CDTXMania.Tx.Taiko_Combo[1].vc拡大縮小倍率.Y = this.nコンボ拡大率_座標_100combo[ this.ctコンボ加算[ nPlayer ].n現在の値, 0 ];
                        CDTXMania.Tx.Taiko_Combo[1].vc拡大縮小倍率.X = 0.7f;
                        CDTXMania.Tx.Taiko_Combo[1].t2D描画( CDTXMania.app.Device, x, ( y + 14 ) + (int)this.nコンボ拡大率_座標_100combo[ this.ctコンボ加算[ nPlayer ].n現在の値, 1 ], new Rectangle( n位の数[ i ] * 50, 0, 50, 70 ) );
                    }
                    if(CDTXMania.Tx.Taiko_Combo_Effect != null )
                    {
                        CDTXMania.Tx.Taiko_Combo_Effect.b加算合成 = true;
                        if( this.ctコンボラメ.n現在の値 > 14 && this.ctコンボラメ.n現在の値 < 26 ) //1
                        {
                            CDTXMania.Tx.Taiko_Combo_Effect.t2D描画( CDTXMania.app.Device, nラメ基準X座標 - 13, (nラメ基準Y座標 + 42)- (int)( 1.1 * this.ctコンボラメ.n現在の値 ) );
                        }
                        if( this.ctコンボラメ.n現在の値 < 13 ) //2
                        {
                            #region[透明度制御]
                            if( this.ctコンボラメ.n現在の値 <= 7 ) CDTXMania.Tx.Taiko_Combo_Effect.n透明度 = 255;
                            else if( this.ctコンボラメ.n現在の値 >= 8 && this.ctコンボラメ.n現在の値 <= 12 ) CDTXMania.Tx.Taiko_Combo_Effect.n透明度 = (int)(204 - ( 43.35 * this.ctコンボラメ.n現在の値 ));
                            #endregion
                            CDTXMania.Tx.Taiko_Combo_Effect.t2D描画( CDTXMania.app.Device, nラメ基準X座標, (nラメ基準Y座標 + 20) - (int)( 1.1 * this.ctコンボラメ.n現在の値 ) );
                        }
                        if( this.ctコンボラメ.n現在の値 > 12 && this.ctコンボラメ.n現在の値 < 19 ) //3
                        {
                            CDTXMania.Tx.Taiko_Combo_Effect.n透明度 = 255;
                            CDTXMania.Tx.Taiko_Combo_Effect.t2D描画( CDTXMania.app.Device, nラメ基準X座標 + 15, (nラメ基準Y座標 + 38) - (int)( 1.1 * this.ctコンボラメ.n現在の値 ) );
                        }
                    }
                }
                else
                {
                    x -= 26;
                    //int nラメ基準Y座標 = 199; //2列目のラメの始点を基準とする。
                    int nラメ基準Y座標 = CDTXMania.Skin.nComboNumberY[nPlayer] - 4; //2列目のラメの始点を基準とする。
                    int nラメ基準X座標 = x + (16 - 9);
                    if (CDTXMania.Tx.Taiko_Combo[1] != null)
                    {
                        // this.txCOMBO太鼓.vc拡大縮小倍率.Y = this.nコンボ拡大率_座標[ this.ctコンボ加算[ nPlayer ].n現在の値, 0 ];
                        CDTXMania.Tx.Taiko_Combo[1].vc拡大縮小倍率.Y = this.nコンボ拡大率_座標_1000combo[this.ctコンボ加算[nPlayer].n現在の値, 0];
                        CDTXMania.Tx.Taiko_Combo[1].vc拡大縮小倍率.X = 0.6f;
                        CDTXMania.Tx.Taiko_Combo[1].t2D描画(CDTXMania.app.Device, (x - 18), (y + 20) + (int)this.nコンボ拡大率_座標_1000combo[this.ctコンボ加算[nPlayer].n現在の値, 1], new Rectangle(n位の数[i] * 50, 0, 50, 70));
                    }
                    if (CDTXMania.Tx.Taiko_Combo_Effect != null)
                    {
                        CDTXMania.Tx.Taiko_Combo_Effect.b加算合成 = true;
                        if (this.ctコンボラメ.n現在の値 > 14 && this.ctコンボラメ.n現在の値 < 26) //1
                        {
                            CDTXMania.Tx.Taiko_Combo_Effect.t2D描画(CDTXMania.app.Device, nラメ基準X座標 - 13, (nラメ基準Y座標 + 42) - (int)(1.1 * this.ctコンボラメ.n現在の値));
                        }
                        if (this.ctコンボラメ.n現在の値 < 13) //2
                        {
                            #region[透明度制御]
                            if (this.ctコンボラメ.n現在の値 <= 7) CDTXMania.Tx.Taiko_Combo_Effect.n透明度 = 255;
                            else if (this.ctコンボラメ.n現在の値 >= 8 && this.ctコンボラメ.n現在の値 <= 12) CDTXMania.Tx.Taiko_Combo_Effect.n透明度 = (int)(204 - (43.35 * this.ctコンボラメ.n現在の値));
                            #endregion
                            CDTXMania.Tx.Taiko_Combo_Effect.t2D描画(CDTXMania.app.Device, nラメ基準X座標, (nラメ基準Y座標 + 20) - (int)(1.1 * this.ctコンボラメ.n現在の値));
                        }
                        if (this.ctコンボラメ.n現在の値 > 12 && this.ctコンボラメ.n現在の値 < 19) //3
                        {
                            CDTXMania.Tx.Taiko_Combo_Effect.n透明度 = 255;
                            CDTXMania.Tx.Taiko_Combo_Effect.t2D描画(CDTXMania.app.Device, nラメ基準X座標 + 15, (nラメ基準Y座標 + 38) - (int)(1.1 * this.ctコンボラメ.n現在の値));
                        }
                        /*
                        x -= 33;
                        int nCombo中心X = 271; //仮置き
                        //int nラメ基準Y座標 = 199; //2列目のラメの始点を基準とする。
                        int nラメ基準Y座標 = CDTXMania.Skin.nComboNumberY[nPlayer] - 4;
                        int nラメ基準X座標 = x + ( 16 - 9 );
                        if( this.txCOMBO太鼓_でかいやつ != null )
                        {
                            this.txCOMBO太鼓_でかいやつ.vc拡大縮小倍率.X = 0.6f;
                            this.txCOMBO太鼓_でかいやつ.vc拡大縮小倍率.Y = 0.6f;
                            int[] arComboX = { nCombo中心X + (16 - 1), nCombo中心X - (16 - 1) };
                            //this.txCOMBO太鼓_でかいやつ.vc拡大縮小倍率.Y = this.nコンボ拡大率_座標[ this.ctコンボ加算[ nPlayer ].n現在の値, 0 ];
                            this.txCOMBO太鼓_でかいやつ.t2D描画( CDTXMania.app.Device, ( x - 9 ), ( y + 12 ) + (int)this.nコンボ拡大率_座標[ this.ctコンボ加算[ nPlayer ].n現在の値, 1 ], new Rectangle( n位の数[ i ] * 50, 0, 50, 70 ) );

                        }
                        if( this.txコンボラメ != null )
                        {
                            this.txコンボラメ.b加算合成 = true;
                            if( this.ctコンボラメ.n現在の値 > 14 && this.ctコンボラメ.n現在の値 < 26 ) //1
                            {
                                this.txコンボラメ.t2D描画( CDTXMania.app.Device, nラメ基準X座標 - 13, (nラメ基準Y座標 + 32) - (int)( 1.1 * this.ctコンボラメ.n現在の値 ) );
                            }
                            if( this.ctコンボラメ.n現在の値 < 13 ) //2
                            {
                                #region[透明度制御]
                                if( this.ctコンボラメ.n現在の値 <= 7 ) this.txコンボラメ.n透明度 = 255;
                                else if( this.ctコンボラメ.n現在の値 >= 8 && this.ctコンボラメ.n現在の値 <= 12 ) this.txコンボラメ.n透明度 = (int)(204 - ( 43.35 * this.ctコンボラメ.n現在の値 ));
                                #endregion
                                this.txコンボラメ.t2D描画( CDTXMania.app.Device, nラメ基準X座標, nラメ基準Y座標 - (int)( 1.1 * this.ctコンボラメ.n現在の値 ) );
                            }
                            if( this.ctコンボラメ.n現在の値 > 12 && this.ctコンボラメ.n現在の値 < 19 ) //3
                            {
                                this.txコンボラメ.n透明度 = 255;
                                this.txコンボラメ.t2D描画( CDTXMania.app.Device, nラメ基準X座標 + 15, (nラメ基準Y座標 + 24) - (int)( 1.1 * this.ctコンボラメ.n現在の値 ) );
                            }
                            */
                    }

                }
            }

			//-----------------
			#endregion
		}

		protected virtual void tコンボ表示_ギター( int nCombo値, int nジャンプインデックス )
		{
		}
		protected virtual void tコンボ表示_ベース( int nCombo値, int nジャンプインデックス )
		{
		}
		protected void tコンボ表示_ギター( int nCombo値, int n表示中央X, int n表示中央Y, int nジャンプインデックス )
		{

		}
		protected void tコンボ表示_ベース( int nCombo値, int n表示中央X, int n表示中央Y, int nジャンプインデックス )
		{

		}
		protected void tコンボ表示_ギターベース( int nCombo値, int n表示中央X, int n表示中央Y, int nジャンプインデックス )
		{
		}


		// CActivity 実装

		public override void On活性化()
		{
			this.n現在のコンボ数 = new STCOMBO() { act = this };
			this.status = new CSTATUS();
            this.ctコンボ加算 = new CCounter[ 4 ];
			for( int i = 0; i < 4; i++ )
			{
				this.status[ i ].e現在のモード = EMode.非表示中;
				this.status[ i ].nCOMBO値 = 0;
				this.status[ i ].n最高COMBO値 = 0;
				this.status[ i ].n現在表示中のCOMBO値 = 0;
				this.status[ i ].n残像表示中のCOMBO値 = 0;
				this.status[ i ].nジャンプインデックス値 = 99999;
				this.status[ i ].n前回の時刻_ジャンプ用 = -1;
				this.status[ i ].nコンボが切れた時刻 = -1;
                this.ctコンボ加算[ i ] = new CCounter( 0, 13, 12, CDTXMania.Timer );
			}
            this.ctコンボラメ = new CCounter( 0, 29, 20, CDTXMania.Timer );
			base.On活性化();
		}
		public override void On非活性化()
		{
			if( this.status != null )
				this.status = null;

			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if( this.b活性化してない )
				return;

			//this.txCOMBOドラム = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlayDrums combo drums.png" ) );
			//this.txCOMBOギター = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlayDrums combo guitar.png" ) );
			//this.txCOMBO太鼓 = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_combo taiko.png" ) );
			//this.txCOMBO太鼓_でかいやつ = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_combo taiko_large.png" ) );
   //         this.txコンボラメ = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_Combo effect.png" ) );

			base.OnManagedリソースの作成();
		}
		public override void OnManagedリソースの解放()
		{
			if( this.b活性化してない )
				return;

			//CDTXMania.tテクスチャの解放( ref this.txCOMBOドラム );
			//CDTXMania.tテクスチャの解放( ref this.txCOMBOギター );
            //CDTXMania.tテクスチャの解放( ref this.txCOMBO太鼓 );
            //CDTXMania.tテクスチャの解放( ref this.txCOMBO太鼓_でかいやつ );
            //CDTXMania.tテクスチャの解放( ref this.txコンボラメ );

			base.OnManagedリソースの解放();
		}
		public override int On進行描画()
		{
			if( this.b活性化してない )
				return 0;

			//for( int i = 3; i >= 0; i-- )
			for( int i = 0; i < 4; i++ )
			{
				EEvent e今回の状態遷移イベント;

				#region [ 前回と今回の COMBO 値から、e今回の状態遷移イベントを決定する。]
				//-----------------
				if( this.status[ i ].n現在表示中のCOMBO値 == this.status[ i ].nCOMBO値 )
				{
					e今回の状態遷移イベント = EEvent.同一数値;
				}
				else if( this.status[ i ].n現在表示中のCOMBO値 > this.status[ i ].nCOMBO値 )
				{
					e今回の状態遷移イベント = EEvent.ミス通知;
				}
				else if( ( this.status[ i ].n現在表示中のCOMBO値 < CDTXMania.ConfigIni.n表示可能な最小コンボ数.Drums ) && ( this.status[ i ].nCOMBO値 < CDTXMania.ConfigIni.n表示可能な最小コンボ数.Drums ) )
				{
					e今回の状態遷移イベント = EEvent.非表示;
				}
				else
				{
					e今回の状態遷移イベント = EEvent.数値更新;
				}
				//-----------------
				#endregion

				#region [ nジャンプインデックス値 の進行。]
				//-----------------
				if( this.status[ i ].nジャンプインデックス値 < 360 )
				{
					if( ( this.status[ i ].n前回の時刻_ジャンプ用 == -1 ) || ( CDTXMania.Timer.n現在時刻 < this.status[ i ].n前回の時刻_ジャンプ用 ) )
						this.status[ i ].n前回の時刻_ジャンプ用 = CDTXMania.Timer.n現在時刻;

					const long INTERVAL = 2;
					while( ( CDTXMania.Timer.n現在時刻 - this.status[ i ].n前回の時刻_ジャンプ用 ) >= INTERVAL )
					{
						if( this.status[ i ].nジャンプインデックス値 < 2000 )
							this.status[ i ].nジャンプインデックス値 += 3;

						this.status[ i ].n前回の時刻_ジャンプ用 += INTERVAL;
					}
				}
			//-----------------
				#endregion


			Retry:	// モードが変化した場合はここからリトライする。

				switch( this.status[ i ].e現在のモード )
				{
					case EMode.非表示中:
						#region [ *** ]
						//-----------------

						if( e今回の状態遷移イベント == EEvent.数値更新 )
						{
							// モード変更
							this.status[ i ].e現在のモード = EMode.進行表示中;
							this.status[ i ].nジャンプインデックス値 = 0;
							this.status[ i ].n前回の時刻_ジャンプ用 = CDTXMania.Timer.n現在時刻;
							goto Retry;
						}

						this.status[ i ].n現在表示中のCOMBO値 = this.status[ i ].nCOMBO値;
						break;
					//-----------------
						#endregion

					case EMode.進行表示中:
						#region [ *** ]
						//-----------------

						if( ( e今回の状態遷移イベント == EEvent.非表示 ) || ( e今回の状態遷移イベント == EEvent.ミス通知 ) )
						{
							// モード変更
							this.status[ i ].e現在のモード = EMode.残像表示中;
							this.status[ i ].n残像表示中のCOMBO値 = this.status[ i ].n現在表示中のCOMBO値;
							this.status[ i ].nコンボが切れた時刻 = CDTXMania.Timer.n現在時刻;
							goto Retry;
						}

						if( e今回の状態遷移イベント == EEvent.数値更新 )
						{
							this.status[ i ].nジャンプインデックス値 = 0;
							this.status[ i ].n前回の時刻_ジャンプ用 = CDTXMania.Timer.n現在時刻;
						}

						this.status[ i ].n現在表示中のCOMBO値 = this.status[ i ].nCOMBO値;
						switch( i )
						{
							case 0:
								this.tコンボ表示_太鼓( this.status[ i ].nCOMBO値, this.status[ i ].nジャンプインデックス値, 0 );
								break;

							case 1:
								this.tコンボ表示_太鼓( this.status[ i ].nCOMBO値, this.status[ i ].nジャンプインデックス値, 1 );
								break;

							case 2:
								this.tコンボ表示_ベース( this.status[ i ].nCOMBO値, this.status[ i ].nジャンプインデックス値 );
								break;

							case 3:
								this.tコンボ表示_ドラム( this.status[ i ].nCOMBO値, this.status[ i ].nジャンプインデックス値 );
								break;
						}
						break;
					//-----------------
						#endregion

					case EMode.残像表示中:
						#region [ *** ]
						//-----------------
						if( e今回の状態遷移イベント == EEvent.数値更新 )
						{
							// モード変更１
							this.status[ i ].e現在のモード = EMode.進行表示中;
							goto Retry;
						}
						if( ( CDTXMania.Timer.n現在時刻 - this.status[ i ].nコンボが切れた時刻 ) > 1000 )
						{
							// モード変更２
							this.status[ i ].e現在のモード = EMode.非表示中;
							goto Retry;
						}
						this.status[ i ].n現在表示中のCOMBO値 = this.status[ i ].nCOMBO値;
						break;
						//-----------------
						#endregion
				}
			}

			return 0;
		}
	}
}
