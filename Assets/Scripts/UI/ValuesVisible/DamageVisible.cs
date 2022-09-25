using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DamageVisible : MonoBehaviour
{
    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private float _liveTime = 2;
    [SerializeField] private float _transparentPerMsecond = 1.5f;

    private void Start()
    {
        transform.SetParent(null);
        transform.localScale = new Vector3(1, 1, 1);
    }

    private void Update()
    {
        _damageText.transform.LookAt(MainLogic.main.player.cam.transform);
        _damageText.transform.Rotate(new Vector3(0, 180, 0));
    }

    private void FixedUpdate()
    {
        _damageText.transform.position += Vector3.up * Time.deltaTime;
        _damageText.color += new Color(_damageText.color.r, _damageText.color.g,
            _damageText.color.b, _damageText.color.a - _transparentPerMsecond) * Time.deltaTime;
    }

    public void SetText(int damage)
    {
        _damageText.text = damage.ToString();

        Destroy(gameObject, _liveTime);
    }
}
