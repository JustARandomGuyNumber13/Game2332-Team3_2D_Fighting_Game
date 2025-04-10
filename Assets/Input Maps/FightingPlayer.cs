//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.2
//     from Assets/Input Maps/FightingPlayer.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @FightingPlayer: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @FightingPlayer()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""FightingPlayer"",
    ""maps"": [
        {
            ""name"": ""Player1"",
            ""id"": ""bcc530ba-b0f0-4267-8dcf-4654b2991cc3"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""bb9e60fa-538c-4eca-b591-4fb8648a8141"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Value"",
                    ""id"": ""93a630df-17fc-42f1-9b59-73e6c2e6c465"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Value"",
                    ""id"": ""c3c0da86-c20b-4a36-8387-e1634807241e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SkillOne"",
                    ""type"": ""Button"",
                    ""id"": ""13bd1221-7cc2-41b7-8101-c054e237b999"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SkillTwo"",
                    ""type"": ""Button"",
                    ""id"": ""34263eaf-11da-4950-a09b-fe89f9d7d64c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SkillThree"",
                    ""type"": ""Button"",
                    ""id"": ""4c531fb8-2b58-46b0-81c8-003f804e9628"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SkillFour"",
                    ""type"": ""Button"",
                    ""id"": ""508efe48-472e-4b9e-8688-dc005508b7ad"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SkillFive"",
                    ""type"": ""Button"",
                    ""id"": ""6770bf13-52ab-4071-b054-7abead2406a3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""67560ea1-6cd8-4b8c-8790-ef74fa29ec1a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""fa0b5912-b9f8-4cb5-9b92-15433daf897d"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""84778204-0009-44f1-8e03-bde2b0877d5c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""4491f9b4-5a3c-49e8-8a9a-39f03b5d3b1f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b4be7a2b-2df2-4506-b598-abd8aa0e52db"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24423c3a-7523-4297-b4a1-c0bdf2e92c4f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1816c8d1-3cf8-4ffd-92ff-177526ba5448"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkillOne"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cbb4d62c-149e-481c-844e-c2803faedca0"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkillTwo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""263bdd3e-f23f-4a1d-a1e8-a5d2a7bf5239"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkillThree"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""db39769b-c1db-4784-80b3-c38a66aed65d"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkillFour"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6d3aa6db-4e43-4187-86d7-1d7b18aa3d24"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkillFive"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""83b7e5b7-19d8-4521-91f4-99b4619a3866"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player2"",
            ""id"": ""73ab423b-d9b3-4433-b7e9-5708b128fbc8"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""9c09a0ec-db89-4c18-9446-c3fd7b26ac78"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Value"",
                    ""id"": ""ac9ac864-f672-4dc6-a25d-f56b805079d6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Value"",
                    ""id"": ""9f27f10c-9362-45b2-9a6a-2240a4ca77c0"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SkillOne"",
                    ""type"": ""Button"",
                    ""id"": ""85ca403b-aba5-45ff-b081-2c9fde87dae5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SkillTwo"",
                    ""type"": ""Button"",
                    ""id"": ""2df3b619-0485-4356-b5c9-c26256c86218"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SkillThree"",
                    ""type"": ""Button"",
                    ""id"": ""cd32cdea-b4f3-42e0-bba0-f9ae2a8fc356"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SkillFour"",
                    ""type"": ""Button"",
                    ""id"": ""85f4de28-6025-47ea-b56c-6c0d7b66abb4"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SkillFive"",
                    ""type"": ""Button"",
                    ""id"": ""2eb9a69c-2a0b-407c-bc52-977f278d9bc5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""2f2e4476-a74d-4c54-80de-daeb2c154668"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""7a5583ec-d151-4eb4-af8c-9474322c5260"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f1da19b1-e9f0-480a-be7d-b065fbb5b53c"",
                    ""path"": ""<Keyboard>/numpad1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""ca9b7ace-f505-40ab-b0ba-523d42d40795"",
                    ""path"": ""<Keyboard>/numpad3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""71408909-2ca8-4a0c-a523-1d904707babb"",
                    ""path"": ""<Keyboard>/numpad5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""268a6d78-c010-4b43-b645-84f69c190a9b"",
                    ""path"": ""<Keyboard>/numpad2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a86c3f99-bc8d-4626-a288-81c16a9a62a6"",
                    ""path"": ""<Keyboard>/numpad7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkillOne"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a7d61b28-feed-407e-86f6-cb18458816e0"",
                    ""path"": ""<Keyboard>/numpad8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkillTwo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""004c3585-6cc0-44c7-851a-a5048e46a0d8"",
                    ""path"": ""<Keyboard>/numpad9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkillThree"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""29ab0eca-6ef2-48dc-accc-8b9b4619fdc7"",
                    ""path"": ""<Keyboard>/numpadPlus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkillFour"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8bf5f26-3ee8-425e-a136-c6ba18eddea2"",
                    ""path"": ""<Keyboard>/numpad6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkillFive"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""50fb7de3-e7c7-4832-a056-fe9a724449b7"",
                    ""path"": ""<Keyboard>/numpad4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player1
        m_Player1 = asset.FindActionMap("Player1", throwIfNotFound: true);
        m_Player1_Move = m_Player1.FindAction("Move", throwIfNotFound: true);
        m_Player1_Jump = m_Player1.FindAction("Jump", throwIfNotFound: true);
        m_Player1_Crouch = m_Player1.FindAction("Crouch", throwIfNotFound: true);
        m_Player1_SkillOne = m_Player1.FindAction("SkillOne", throwIfNotFound: true);
        m_Player1_SkillTwo = m_Player1.FindAction("SkillTwo", throwIfNotFound: true);
        m_Player1_SkillThree = m_Player1.FindAction("SkillThree", throwIfNotFound: true);
        m_Player1_SkillFour = m_Player1.FindAction("SkillFour", throwIfNotFound: true);
        m_Player1_SkillFive = m_Player1.FindAction("SkillFive", throwIfNotFound: true);
        m_Player1_Attack = m_Player1.FindAction("Attack", throwIfNotFound: true);
        // Player2
        m_Player2 = asset.FindActionMap("Player2", throwIfNotFound: true);
        m_Player2_Move = m_Player2.FindAction("Move", throwIfNotFound: true);
        m_Player2_Jump = m_Player2.FindAction("Jump", throwIfNotFound: true);
        m_Player2_Crouch = m_Player2.FindAction("Crouch", throwIfNotFound: true);
        m_Player2_SkillOne = m_Player2.FindAction("SkillOne", throwIfNotFound: true);
        m_Player2_SkillTwo = m_Player2.FindAction("SkillTwo", throwIfNotFound: true);
        m_Player2_SkillThree = m_Player2.FindAction("SkillThree", throwIfNotFound: true);
        m_Player2_SkillFour = m_Player2.FindAction("SkillFour", throwIfNotFound: true);
        m_Player2_SkillFive = m_Player2.FindAction("SkillFive", throwIfNotFound: true);
        m_Player2_Attack = m_Player2.FindAction("Attack", throwIfNotFound: true);
    }

    ~@FightingPlayer()
    {
        UnityEngine.Debug.Assert(!m_Player1.enabled, "This will cause a leak and performance issues, FightingPlayer.Player1.Disable() has not been called.");
        UnityEngine.Debug.Assert(!m_Player2.enabled, "This will cause a leak and performance issues, FightingPlayer.Player2.Disable() has not been called.");
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player1
    private readonly InputActionMap m_Player1;
    private List<IPlayer1Actions> m_Player1ActionsCallbackInterfaces = new List<IPlayer1Actions>();
    private readonly InputAction m_Player1_Move;
    private readonly InputAction m_Player1_Jump;
    private readonly InputAction m_Player1_Crouch;
    private readonly InputAction m_Player1_SkillOne;
    private readonly InputAction m_Player1_SkillTwo;
    private readonly InputAction m_Player1_SkillThree;
    private readonly InputAction m_Player1_SkillFour;
    private readonly InputAction m_Player1_SkillFive;
    private readonly InputAction m_Player1_Attack;
    public struct Player1Actions
    {
        private @FightingPlayer m_Wrapper;
        public Player1Actions(@FightingPlayer wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player1_Move;
        public InputAction @Jump => m_Wrapper.m_Player1_Jump;
        public InputAction @Crouch => m_Wrapper.m_Player1_Crouch;
        public InputAction @SkillOne => m_Wrapper.m_Player1_SkillOne;
        public InputAction @SkillTwo => m_Wrapper.m_Player1_SkillTwo;
        public InputAction @SkillThree => m_Wrapper.m_Player1_SkillThree;
        public InputAction @SkillFour => m_Wrapper.m_Player1_SkillFour;
        public InputAction @SkillFive => m_Wrapper.m_Player1_SkillFive;
        public InputAction @Attack => m_Wrapper.m_Player1_Attack;
        public InputActionMap Get() { return m_Wrapper.m_Player1; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player1Actions set) { return set.Get(); }
        public void AddCallbacks(IPlayer1Actions instance)
        {
            if (instance == null || m_Wrapper.m_Player1ActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_Player1ActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Crouch.started += instance.OnCrouch;
            @Crouch.performed += instance.OnCrouch;
            @Crouch.canceled += instance.OnCrouch;
            @SkillOne.started += instance.OnSkillOne;
            @SkillOne.performed += instance.OnSkillOne;
            @SkillOne.canceled += instance.OnSkillOne;
            @SkillTwo.started += instance.OnSkillTwo;
            @SkillTwo.performed += instance.OnSkillTwo;
            @SkillTwo.canceled += instance.OnSkillTwo;
            @SkillThree.started += instance.OnSkillThree;
            @SkillThree.performed += instance.OnSkillThree;
            @SkillThree.canceled += instance.OnSkillThree;
            @SkillFour.started += instance.OnSkillFour;
            @SkillFour.performed += instance.OnSkillFour;
            @SkillFour.canceled += instance.OnSkillFour;
            @SkillFive.started += instance.OnSkillFive;
            @SkillFive.performed += instance.OnSkillFive;
            @SkillFive.canceled += instance.OnSkillFive;
            @Attack.started += instance.OnAttack;
            @Attack.performed += instance.OnAttack;
            @Attack.canceled += instance.OnAttack;
        }

        private void UnregisterCallbacks(IPlayer1Actions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Crouch.started -= instance.OnCrouch;
            @Crouch.performed -= instance.OnCrouch;
            @Crouch.canceled -= instance.OnCrouch;
            @SkillOne.started -= instance.OnSkillOne;
            @SkillOne.performed -= instance.OnSkillOne;
            @SkillOne.canceled -= instance.OnSkillOne;
            @SkillTwo.started -= instance.OnSkillTwo;
            @SkillTwo.performed -= instance.OnSkillTwo;
            @SkillTwo.canceled -= instance.OnSkillTwo;
            @SkillThree.started -= instance.OnSkillThree;
            @SkillThree.performed -= instance.OnSkillThree;
            @SkillThree.canceled -= instance.OnSkillThree;
            @SkillFour.started -= instance.OnSkillFour;
            @SkillFour.performed -= instance.OnSkillFour;
            @SkillFour.canceled -= instance.OnSkillFour;
            @SkillFive.started -= instance.OnSkillFive;
            @SkillFive.performed -= instance.OnSkillFive;
            @SkillFive.canceled -= instance.OnSkillFive;
            @Attack.started -= instance.OnAttack;
            @Attack.performed -= instance.OnAttack;
            @Attack.canceled -= instance.OnAttack;
        }

        public void RemoveCallbacks(IPlayer1Actions instance)
        {
            if (m_Wrapper.m_Player1ActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayer1Actions instance)
        {
            foreach (var item in m_Wrapper.m_Player1ActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_Player1ActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public Player1Actions @Player1 => new Player1Actions(this);

    // Player2
    private readonly InputActionMap m_Player2;
    private List<IPlayer2Actions> m_Player2ActionsCallbackInterfaces = new List<IPlayer2Actions>();
    private readonly InputAction m_Player2_Move;
    private readonly InputAction m_Player2_Jump;
    private readonly InputAction m_Player2_Crouch;
    private readonly InputAction m_Player2_SkillOne;
    private readonly InputAction m_Player2_SkillTwo;
    private readonly InputAction m_Player2_SkillThree;
    private readonly InputAction m_Player2_SkillFour;
    private readonly InputAction m_Player2_SkillFive;
    private readonly InputAction m_Player2_Attack;
    public struct Player2Actions
    {
        private @FightingPlayer m_Wrapper;
        public Player2Actions(@FightingPlayer wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player2_Move;
        public InputAction @Jump => m_Wrapper.m_Player2_Jump;
        public InputAction @Crouch => m_Wrapper.m_Player2_Crouch;
        public InputAction @SkillOne => m_Wrapper.m_Player2_SkillOne;
        public InputAction @SkillTwo => m_Wrapper.m_Player2_SkillTwo;
        public InputAction @SkillThree => m_Wrapper.m_Player2_SkillThree;
        public InputAction @SkillFour => m_Wrapper.m_Player2_SkillFour;
        public InputAction @SkillFive => m_Wrapper.m_Player2_SkillFive;
        public InputAction @Attack => m_Wrapper.m_Player2_Attack;
        public InputActionMap Get() { return m_Wrapper.m_Player2; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player2Actions set) { return set.Get(); }
        public void AddCallbacks(IPlayer2Actions instance)
        {
            if (instance == null || m_Wrapper.m_Player2ActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_Player2ActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Crouch.started += instance.OnCrouch;
            @Crouch.performed += instance.OnCrouch;
            @Crouch.canceled += instance.OnCrouch;
            @SkillOne.started += instance.OnSkillOne;
            @SkillOne.performed += instance.OnSkillOne;
            @SkillOne.canceled += instance.OnSkillOne;
            @SkillTwo.started += instance.OnSkillTwo;
            @SkillTwo.performed += instance.OnSkillTwo;
            @SkillTwo.canceled += instance.OnSkillTwo;
            @SkillThree.started += instance.OnSkillThree;
            @SkillThree.performed += instance.OnSkillThree;
            @SkillThree.canceled += instance.OnSkillThree;
            @SkillFour.started += instance.OnSkillFour;
            @SkillFour.performed += instance.OnSkillFour;
            @SkillFour.canceled += instance.OnSkillFour;
            @SkillFive.started += instance.OnSkillFive;
            @SkillFive.performed += instance.OnSkillFive;
            @SkillFive.canceled += instance.OnSkillFive;
            @Attack.started += instance.OnAttack;
            @Attack.performed += instance.OnAttack;
            @Attack.canceled += instance.OnAttack;
        }

        private void UnregisterCallbacks(IPlayer2Actions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Crouch.started -= instance.OnCrouch;
            @Crouch.performed -= instance.OnCrouch;
            @Crouch.canceled -= instance.OnCrouch;
            @SkillOne.started -= instance.OnSkillOne;
            @SkillOne.performed -= instance.OnSkillOne;
            @SkillOne.canceled -= instance.OnSkillOne;
            @SkillTwo.started -= instance.OnSkillTwo;
            @SkillTwo.performed -= instance.OnSkillTwo;
            @SkillTwo.canceled -= instance.OnSkillTwo;
            @SkillThree.started -= instance.OnSkillThree;
            @SkillThree.performed -= instance.OnSkillThree;
            @SkillThree.canceled -= instance.OnSkillThree;
            @SkillFour.started -= instance.OnSkillFour;
            @SkillFour.performed -= instance.OnSkillFour;
            @SkillFour.canceled -= instance.OnSkillFour;
            @SkillFive.started -= instance.OnSkillFive;
            @SkillFive.performed -= instance.OnSkillFive;
            @SkillFive.canceled -= instance.OnSkillFive;
            @Attack.started -= instance.OnAttack;
            @Attack.performed -= instance.OnAttack;
            @Attack.canceled -= instance.OnAttack;
        }

        public void RemoveCallbacks(IPlayer2Actions instance)
        {
            if (m_Wrapper.m_Player2ActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayer2Actions instance)
        {
            foreach (var item in m_Wrapper.m_Player2ActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_Player2ActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public Player2Actions @Player2 => new Player2Actions(this);
    public interface IPlayer1Actions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnSkillOne(InputAction.CallbackContext context);
        void OnSkillTwo(InputAction.CallbackContext context);
        void OnSkillThree(InputAction.CallbackContext context);
        void OnSkillFour(InputAction.CallbackContext context);
        void OnSkillFive(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
    }
    public interface IPlayer2Actions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnSkillOne(InputAction.CallbackContext context);
        void OnSkillTwo(InputAction.CallbackContext context);
        void OnSkillThree(InputAction.CallbackContext context);
        void OnSkillFour(InputAction.CallbackContext context);
        void OnSkillFive(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
    }
}
