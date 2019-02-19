/*
 * CameraControl ：相机控制器，控制相机跟随角色，有防穿墙处理，有相机自动转向功能
 * 程序员 ：Wilson
 * 日期 ：2018/12/2
 * 挂载对象 ：（Scene）Battle / Player / CameraController / CameraPos
 * 更多描述 ：A.穿墙拉近时相机位置会变更，多创建一个物体（KeepCameraPos）保存着相机原有的位置，当不触发穿墙时，
 *           可以将相机位置重新插值会原来的位置。
 * 			 B.角色相机的控制借鉴于APRG游戏 '巫师3'
 * 修改记录 ：
 */


using M_CharactorSystem;
using M_CharactorSystem.M_Player;
using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
	[Header("===== 相机灵敏度 ======")]
	public float AxixSpeed = 150;
	public float AxiySpeed = 150;
	
	//镜头缩放范围
	public float MaxSuo = 100.0f;
	public float MinSuo = 35f;
	
	public float DampTime = 0.1f; //相机延迟时间
	public bool LockState; //当前是否锁定
	public float ObjectDistance;
	public Image LockIco;

	#region 类私有变量
	
	private GameObject _keepCameraPosOb; //保持相机与角色原本距离的物件
	private GameObject _walkPosOb; //奔跑拉远距离坐标物件（弃用）
	private GameObject _cameraController; //相机控制器
	private GameObject _camera; //主相机
	private IPlayer _player; //角色对象
	private GameObject _playerController; //角色控制器
	private GameObject _model; //角色模型
	private float _tempEuler; //保存限制过后的欧拉角
	private bool _isDie = false; //是否死亡
	private Vector3 _cameraDampVelocity;
	private bool _bHit = false; 
	private float _distanceAway; //镜头与角色距离
	public LockGameObject LockTarget = null;
	

	#endregion
	
	private void Start()
	{
		_camera = Camera.main.gameObject;
	    _cameraController = transform.parent.gameObject;
		_playerController = _cameraController.transform.parent.gameObject;
		_keepCameraPosOb = _cameraController.transform.Find("KeepCameraPos").gameObject;
		_walkPosOb = _cameraController.transform.Find("CameraWalkPos").gameObject;
		//_model = _playerController.GetComponent<ICharactor>().GetModel();
		//_player = _playerController.GetComponent<Player>();
		_distanceAway = Vector3.Distance(transform.position, _playerController.transform.position);
	}

	public void SetModel(GameObject theGo)
	{
		_model = theGo;
	}

	public void SetCharactor(ICharactor theCharactor)
	{
		_player = theCharactor as IPlayer;
	}

	private void FixedUpdate()
	{
		if (!LockState) //非相机锁定
		{
			//将人物model的欧拉角保存在临时变量
			var tempModelEuler = _model.transform.eulerAngles;

			if (!_isDie) //非死亡状态下
			{
				//旋转人物控制柄(即手动控制相机旋转)
				_playerController.transform.Rotate(Vector3.up,
					_player.Controller.Jright * AxixSpeed * Time.fixedDeltaTime);
				
				//随着角色模型的移动旋转相机（即自动旋转相机，不过只有X轴移动才会旋转）
				_playerController.transform.Rotate(Vector3.up,
					(_player.Controller.GetVelocityX()) * AxixSpeed * 0.5f * Time.fixedDeltaTime);

			
				//绕x轴旋转度数进行限制
				_tempEuler -= _player.Controller.Jup * AxiySpeed * Time.fixedDeltaTime;
				_tempEuler = Mathf.Clamp(_tempEuler, -60f, 60f);
				_cameraController.transform.localEulerAngles = new Vector3(_tempEuler, 0, 0);
				
			}

			//将人物的欧拉角归位
			_model.transform.eulerAngles = tempModelEuler;
			
			CameraFollow(DampTime);
		}
		
		else //相机锁定
		{
			//将人物控制柄的z轴对准locktarget
			var tempPos = LockTarget.Obj.transform.position - _model.transform.position;
			tempPos.y = 0;
			_playerController.transform.forward = tempPos;

			//让相机控制柄盯着locktarget的脚底
			_cameraController.transform.LookAt(LockTarget.Obj.transform);

			//规定相机能调整的范围
			var tempX = Mathf.Clamp(_cameraController.transform.localEulerAngles.x, 0f, 10f);
			_cameraController.transform.localEulerAngles = new Vector3(tempX, 0, 0);
				
			CameraFollow(0);
		}
		
		//走路的时候拉近相机位置
		if (_player.Controller.DMag <= 0.8f && _player.Controller.DMag > 0f)
		{
			_camera.transform.position = Vector3.SmoothDamp(_camera.transform.position, _walkPosOb.transform.position,
				ref _cameraDampVelocity, DampTime);
		}
		
		//相机指向相机控制柄
		_camera.transform.LookAt(_cameraController.transform);
		
	}//FixedUpdate_End

	private void Update()
	{
		//缩放
		//Zoom();

		//计算角色与所锁定物体的距离
		if (LockTarget != null)
		{
			ObjectDistance = Vector3.Distance(_model.transform.position, LockTarget.Obj.transform.position);
		}

		//距离过远时解除相机锁定
		if (ObjectDistance >= 15)
		{
			OnLock(null, false, false);
		}

		//计算角色与相机之间的距离
		_distanceAway = Vector3.Distance(transform.position, _playerController.transform.position);

		//射线，用于解决‘穿墙问题’
		RaycastHit hit;
		if (Physics.Linecast(_playerController.transform.position + Vector3.up, transform.position, out hit,
			~(1 << 12 | 1 << 13)))
		{
			var tagname = hit.collider.gameObject.tag;
			if (tagname != "MainCamera")
			{
				//如果射线碰撞的不是相机，那么就取得射线碰撞点到玩家的距离
				float currentDistance = Vector3.Distance(hit.point, _playerController.transform.position);

				//如果射线碰撞点小于玩家与相机本来的距离，就说明角色身后是有东西，为了避免穿墙，就把相机拉近
				if (currentDistance < _distanceAway)
				{
					transform.position = hit.point;
				}
			}

		}
		else //将相机恢复到原来的位置
		{
			if (transform.position != _keepCameraPosOb.transform.position)
			{
				transform.position = Vector3.Lerp(transform.position, _keepCameraPosOb.transform.position, 0.1f);
			}
		}

	}


	#region 类私有方法
	
	//按鼠标滚轮缩放
	void Zoom()
	{
		//实现滑轮拖动
		if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			if (Camera.main.fieldOfView < MaxSuo)//缩放的范围
			{
				Camera.main.fieldOfView += 2;
			}
		}
 
		//Zoom in
		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			if (Camera.main.fieldOfView > MinSuo)
			{
				Camera.main.fieldOfView -= 2;
			}
		}
	}

	//相机延迟速度
	void CameraFollow(float dampTime)
	{
		_camera.transform.position = Vector3.SmoothDamp(_camera.transform.position, transform.position,
			ref _cameraDampVelocity, dampTime);
	}
	
	//角度修正
//	private float FixAngle(float value)
//	{
//		//将大于180度角进行以负数形式输出
//		float angle = value - 180;
//
//		if (angle > 0)
//		{
//			return angle - 180;
//		}
//
//		if (value == 0)
//		{
//			return 0;
//		}
//
//		return angle + 180;
//	}
	
	#endregion
	
	/// <summary>
	/// 锁定敌人的功能
	/// </summary>
	public void LockUnLock()
	{

		//箱型不可见框的参数
		Vector3 modelOrigin1 = _model.transform.position;
		Vector3 modelOrigin2 = _model.transform.position + new Vector3(0, 1, 0);
		Vector3 center = modelOrigin2 + _model.transform.forward * 5;
		
		//创建一个箱型不可见框，通过输出与框接触的任何碰撞器来测试碰撞
		Collider[] cols = Physics.OverlapBox(center, new Vector3(0.5f, 0.5f, 5), _model.transform.rotation,
			LayerMask.GetMask("Enemy"));

		//没有锁定任何敌人
		if (cols.Length == 0)
		{
			OnLock(null, false, false);
		}

		foreach (var col in cols)
		{
			if (LockTarget != null && LockTarget.Obj == col.gameObject)
			{
				OnLock(null, false, false);
				break;
			}
			OnLock(new LockGameObject(col.gameObject, col.bounds.extents.y), true, true);
			break;
		}
	}

	//解锁
	void OnLock(LockGameObject _lockTarget, bool _lockico, bool _lockstate)
	{
		LockTarget = _lockTarget;
        //LockIco.enabled = _lockico;
        LockState = _lockstate;
	}





}
