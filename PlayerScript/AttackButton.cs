using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    int attackCount = 1;
    float doubleAttackPercentage = 0.0f;
    AudioScript _audioScript;
    MapArea mapArea;
    [HideInInspector] public Move move;
    Move[] moves;
    public EnemyHP enemy;
    UI_Script uiScript;
    bool flag = true;

    void Start()
    {
        uiScript = FindObjectOfType<UI_Script>();
        _audioScript = FindObjectOfType<AudioScript>();
        mapArea=FindObjectOfType<MapArea>();
        moves = FindObjectsOfType<Move>();
    }

    void Update()
    {
        moves = FindObjectsOfType<Move>();
    }

    public void OnAttack()
    {
        move = GetSelectedMove();

        if (move.count >= 1 && flag)
        {
            StartAttackSequence();
        }
        else if (move.count <= 0)
        {
            HandleFailedAttack();
        }
    }

    public void OnMagic()
    {
        move = GetSelectedMove();

        if (move.count >= 1 && flag)
        {
            mapArea.DestroyCube();
            StartMagicSequence();
        }
        else if (move.count <= 0)
        {
            mapArea.DestroyCube();
            HandleFailedAttack();
        }
    }

    Move GetSelectedMove()
    {
        foreach (Move m in moves)
        {
            if (m.Player_Select)
            {
                return m;
            }
        }
        return null;
    }

    void StartAttackSequence()
    {
        uiScript.endButton.SetActive(false);
        uiScript.magicButton.SetActive(false);
        _audioScript.audio_button.Play();
        move.StopBool = true;
        flag = false;
        doubleAttackPercentage = 0.0f;
        IsDoubleAttack(move.count);

        attackCount = (doubleAttackPercentage >= Random.value) ? 2 : 1;

        enemy = GetFirstAvailableEnemy(move.list_ColliderEnemy);

        if (enemy != null && enemy.bool_isattack)
        {
            StartCoroutine(EnemyDamageRoutine(enemy, move));
            SetAllMovesEnemyClick(false);
        }
        else
        {
            ResetMoveAndFlag();
        }
    }

    void StartMagicSequence()
    {
        uiScript.endButton.SetActive(false);
        uiScript.attackButton.SetActive(false);
        _audioScript.audio_button.Play();
        move.StopBool = true;
        flag = false;

        enemy = move.masicToPlayer 
            ? GetFirstAvailableEnemy(move.list_ColliderPlayer) 
            : GetFirstAvailableEnemy(move.list_ColliderEnemy);

        if (enemy != null)
        {
            StartCoroutine(EnemyMagicDamageRoutine(enemy, move));
            SetAllMovesEnemyClick(false);
        }
        else
        {
            ResetMoveAndFlag();
        }
    }

    EnemyHP GetFirstAvailableEnemy(List<EnemyHP> colliders)
    {
        foreach (var collider in colliders)
        {
            if (collider != null)
            {
                return collider.GetComponent<EnemyHP>();
            }
        }
        return null;
    }

    void HandleFailedAttack()
    {
        EnemyHP.manybool_action = 7;
        _audioScript.audio_fail.Play();
    }

    void SetAllMovesEnemyClick(bool value)
    {

        foreach (var m in moves)
        {
            m.bool_EnemyClick = value;
        }
    }

    void ResetMoveAndFlag()
    {
        move.StopBool = false;
        flag = true;
    }

    IEnumerator EnemyDamageRoutine(EnemyHP enemy, Move move)
    {
        for (int a = 1; a <= attackCount; a++)
        {
            if (enemy != null && enemy.currentHP >= 1)
            {
                yield return new WaitForSeconds(0.1f);
                EnemyHP.manybool_action = (a == 1) ? -1 : 8;
                move.transform.position += move.directionAttackMove(move.CancelDirection);

                yield return FlashEffect(enemy, 3);

                StartCoroutine(enemy.TakeDamage(move.AttackPoint));

                if (!enemy.bool_avoid)
                {
                    StartCoroutine(DamageEffect(enemy));
                }

                yield return new WaitForSeconds(0.6f);
                move.transform.position -= move.directionAttackMove(move.CancelDirection);
               
                SetAllMovesEnemyClick(true);
            }
        }
         move.count = 0;
        move.count_F = move.count; 
        flag = true;
        uiScript.endButton.SetActive(true);
        uiScript.attackButton.SetActive(false);
        move.StopBool = false;
    }

    IEnumerator EnemyMagicDamageRoutine(EnemyHP enemy, Move move)
    {
        yield return FlashEffect(enemy, 3);
        StartCoroutine(enemy.TakeDamage_Magic(move.Player_Number, move.AttackPoint, move.gameObject));

        yield return new WaitForSeconds(0.6f);
        move.count = 0;
        move.count_F = move.count;
        flag = true;
        SetAllMovesEnemyClick(true);
        uiScript.endButton.SetActive(true);
        uiScript.magicButton.SetActive(false);
        move.StopBool = false;
    }

    IEnumerator FlashEffect(EnemyHP enemy, int flashes)
    {
        for (int i = 0; i < flashes; i++)
        {
            enemy.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            yield return new WaitForSeconds(0.1f);
            enemy.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator DamageEffect(EnemyHP enemy)
    {
        if (EnemyHP.manybool_action != 5)
        {
            enemy.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            yield return new WaitForSeconds(0.3f);
            enemy.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }

    void IsDoubleAttack(int count)
    {
        doubleAttackPercentage = count switch
        {
            1 => 0.0f,
            2 => 0.33f,
            3 => 0.66f,
            _ => 0.99f
        };
    }
    
}
