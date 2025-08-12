
### 프로젝트 결과 보고서

----------

### 1. 구현된 핵심 기능


### 1.1 기본 이동 및 점프,대시 시스템 🏃‍♂️

-   **이동**: `PlayerController.cs` 스크립트에서 `OnMoveInput` 함수를 통해 `Input System`의 입력 값을 받고, `FixedUpdate`의 `Move()` 함수를 통해 `Rigidbody`의 `velocity`를 직접 제어해 플레이어를 이동시켰습니다.
    
-   **점프**: `OnJumpInput` 함수는 `IsGrounded()` 메서드로 지면 착지 상태를 확인한 후, `Rigidbody.AddForce()`와 `ForceMode.Impulse`를 사용하여 순간적인 힘을 가해 점프를 구현했습니다.
    
-   **대시**: `OnDashInput` 함수는 `PlayerCondition` 스크립트의 `UseStamina()` 메서드를 호출하여 스태미나 소모 여부를 확인합니다. 스태미나가 충분할 경우, `DashCoroutine`을 시작하여 `PlayerCondition`의 `speedMultiplier` 값을 일정 시간 동안 증가시켜 플레이어가 빠르게 이동하는 효과를 구현했습니다.
    


#### 1.2 체력/스태미나 바 UI 💖

-   **UI 연동**: `PlayerCondition.cs` 스크립트는 `UICondition` 컴포넌트를 통해 체력, 허기, 스태미나 값을 관리합니다. 이 값들은 `Condition.cs` 스크립트의 `Update` 함수에서 매 프레임 UI `Image`의 `fillAmount`를 갱신하여 실시간으로 상태를 표시합니다.
    
-   **스태미나 소모**: `PlayerController.cs`의 `OnJumpInput` 함수와 `OnDashInput` 함수는 모두 `PlayerCondition.UseStamina()`를 호출하여 점프 및 대시 시 스태미나를 소모합니다. 스태미나가 충분할 때만 각 행동이 가능하며, 필요한 스태미나 양은 `jumpStaminaCost`와 `dashStaminaCost` 변수를 통해 Inspector에서 조절할 수 있습니다.
    

#### 1.3 동적 환경 조사 UI ✨

-   `Interaction.cs` 스크립트는 **`Raycast`**를 사용하여 플레이어가 바라보는 오브젝트를 감지합니다.
    
-   감지된 오브젝트가 **`IInteractable` 인터페이스**를 구현하고 있을 경우, `SetPromptText()` 함수를 호출하여 해당 오브젝트의 상호작용 문구(`GetInteractPrompt()`)를 UI에 표시합니다.
    
-   `OnInteractInput()` 함수가 호출되면, `IInteractable`의 `OnInteract()` 메서드를 실행하여 상호작용을 처리합니다.
    

#### 1.4 점프대 구현 🚀

-   `OnCollisionEnter` 트리거와 `Rigidbody.AddForce(ForceMode.Impulse)`를 사용해, 캐릭터가 밟았을 때 위로 튀어 오르는 점프대를 구현했습니다.
    

#### 1.5 아이템 데이터 및 인벤토리 시스템 🎒

-   **`ScriptableObject`**: `ItemData`를 `ScriptableObject`로 정의하여 아이템의 이름, 설명, 속성 등의 데이터를 에디터에서 유연하게 관리할 수 있는 시스템을 구축했습니다.
    
-   **인벤토리 UI**: `UIInventory.cs`는 `ItemData`를 활용하여 인벤토리 창을 관리합니다. `OnUseButton()` 함수는 아이템의 종류에 따라 `PlayerCondition.cs`의 `Heal()`, `Eat()` 등의 메서드를 호출하며 효과를 적용합니다.
    

#### 1.6 아이템 사용 효과 (코루틴) ✨

-   `UIInventory.cs`의 `OnUseButton()` 함수에서 `StartCoroutine(condition.SpeedBoost(...))`를 호출하여, 아이템 사용 후 `PlayerCondition.cs`의 `speedMultiplier` 값을 일정 시간 동안 증가시키는 기능을 구현했습니다.
    

----------

### 2. 트러블슈팅: 곰이 제자리걸음만 하는 현상 해결 🐻

-   **문제 현상**: 곰 몬스터를 플레이어 근처에 배치했을 때, 플레이어를 추적하지 않고 걷기 애니메이션만 반복하는 현상이 발생했습니다.
    
-   **원인 분석**: `NPC.cs` 스크립트의 `AttackingUpdate()` 함수를 보면, `playerDistance < attackDistance`일 때 `NavMeshAgent`의 `isStopped`를 `true`로 설정하여 몬스터의 이동을 멈추도록 되어 있습니다. 게임 시작 시 곰과 플레이어의 거리가 `attackDistance`보다 가까웠기 때문에, 곰은 움직이기도 전에 멈춤 상태에 진입하여 제자리걸음 애니메이션만 반복했던 것입니다.
    
-   **해결 방법**:  초기 배치 시 곰을 플레이어로부터 충분히 멀리 떨어뜨려 놓아 `Wandering` 상태에서 시작하도록 했습니다.

  

-   **개선 방안**:`NavMeshAgent`의 `Stopping Distance`와 같은 세부 파라미터가 AI의 행동에 미치는 영향을 직접 확인했습니다. 앞으로는 AI를 구현할 때 이 값들을 세밀하게 조정하여 의도한 대로 동작하도록 하겠습니다.
    

    

----------

