#define ICALL_TABLE_corlib 1

static int corlib_icall_indexes [] = {
220,
233,
234,
235,
236,
237,
238,
239,
240,
241,
244,
245,
246,
423,
424,
425,
453,
454,
455,
482,
483,
484,
601,
602,
603,
606,
644,
645,
646,
647,
648,
652,
654,
656,
658,
664,
672,
673,
674,
675,
676,
677,
678,
679,
680,
681,
682,
683,
684,
685,
686,
687,
688,
690,
691,
692,
693,
694,
695,
696,
793,
794,
795,
796,
797,
798,
799,
800,
801,
802,
803,
804,
805,
806,
807,
808,
809,
811,
812,
813,
814,
815,
816,
817,
879,
888,
889,
960,
967,
970,
972,
977,
978,
980,
981,
985,
986,
988,
989,
992,
993,
994,
997,
999,
1002,
1004,
1006,
1015,
1084,
1086,
1088,
1098,
1099,
1100,
1102,
1108,
1109,
1110,
1111,
1112,
1120,
1121,
1122,
1126,
1127,
1129,
1133,
1134,
1135,
1432,
1622,
1623,
10091,
10092,
10094,
10095,
10096,
10097,
10098,
10099,
10101,
10102,
10103,
10104,
10105,
10123,
10125,
10133,
10135,
10137,
10139,
10142,
10193,
10194,
10196,
10197,
10198,
10199,
10200,
10202,
10204,
11407,
11411,
11413,
11414,
11415,
11416,
11858,
11859,
11860,
11861,
11879,
11880,
11881,
11927,
11995,
11998,
12007,
12008,
12009,
12010,
12011,
12012,
12359,
12360,
12365,
12366,
12401,
12444,
12451,
12458,
12469,
12473,
12499,
12582,
12584,
12595,
12597,
12598,
12599,
12606,
12621,
12641,
12642,
12650,
12652,
12659,
12660,
12663,
12665,
12670,
12676,
12677,
12684,
12686,
12698,
12701,
12702,
12703,
12714,
12724,
12730,
12731,
12732,
12734,
12735,
12752,
12754,
12769,
12791,
12792,
12817,
12822,
12852,
12853,
13482,
13496,
13585,
13586,
13808,
13809,
13816,
13817,
13818,
13824,
13894,
14432,
14433,
14863,
14864,
14865,
14871,
14881,
15895,
15916,
15918,
15920,
};
void ves_icall_System_Array_InternalCreate (int,int,int,int,int);
int ves_icall_System_Array_GetCorElementTypeOfElementTypeInternal (int);
int ves_icall_System_Array_IsValueOfElementTypeInternal (int,int);
int ves_icall_System_Array_CanChangePrimitive (int,int,int);
int ves_icall_System_Array_FastCopy (int,int,int,int,int);
int ves_icall_System_Array_GetLengthInternal_raw (int,int,int);
int ves_icall_System_Array_GetLowerBoundInternal_raw (int,int,int);
void ves_icall_System_Array_GetGenericValue_icall (int,int,int);
void ves_icall_System_Array_GetValueImpl_raw (int,int,int,int);
void ves_icall_System_Array_SetGenericValue_icall (int,int,int);
void ves_icall_System_Array_SetValueImpl_raw (int,int,int,int);
void ves_icall_System_Array_InitializeInternal_raw (int,int);
void ves_icall_System_Array_SetValueRelaxedImpl_raw (int,int,int,int);
void ves_icall_System_Runtime_RuntimeImports_ZeroMemory (int,int);
void ves_icall_System_Runtime_RuntimeImports_Memmove (int,int,int);
void ves_icall_System_Buffer_BulkMoveWithWriteBarrier (int,int,int,int);
int ves_icall_System_Delegate_AllocDelegateLike_internal_raw (int,int);
int ves_icall_System_Delegate_CreateDelegate_internal_raw (int,int,int,int,int);
int ves_icall_System_Delegate_GetVirtualMethod_internal_raw (int,int);
void ves_icall_System_Enum_GetEnumValuesAndNames_raw (int,int,int,int);
int ves_icall_System_Enum_InternalGetCorElementType (int);
void ves_icall_System_Enum_InternalGetUnderlyingType_raw (int,int,int);
int ves_icall_System_Environment_get_ProcessorCount ();
int ves_icall_System_Environment_get_TickCount ();
int64_t ves_icall_System_Environment_get_TickCount64 ();
void ves_icall_System_Environment_FailFast_raw (int,int,int,int);
int ves_icall_System_GC_GetCollectionCount (int);
int ves_icall_System_GC_GetMaxGeneration ();
void ves_icall_System_GC_register_ephemeron_array_raw (int,int);
int ves_icall_System_GC_get_ephemeron_tombstone_raw (int);
int64_t ves_icall_System_GC_GetTotalAllocatedBytes_raw (int,int);
void ves_icall_System_GC_SuppressFinalize_raw (int,int);
void ves_icall_System_GC_ReRegisterForFinalize_raw (int,int);
void ves_icall_System_GC_GetGCMemoryInfo (int,int,int,int,int,int);
int ves_icall_System_GC_AllocPinnedArray_raw (int,int,int);
int ves_icall_System_Object_MemberwiseClone_raw (int,int);
double ves_icall_System_Math_Acos (double);
double ves_icall_System_Math_Acosh (double);
double ves_icall_System_Math_Asin (double);
double ves_icall_System_Math_Asinh (double);
double ves_icall_System_Math_Atan (double);
double ves_icall_System_Math_Atan2 (double,double);
double ves_icall_System_Math_Atanh (double);
double ves_icall_System_Math_Cbrt (double);
double ves_icall_System_Math_Ceiling (double);
double ves_icall_System_Math_Cos (double);
double ves_icall_System_Math_Cosh (double);
double ves_icall_System_Math_Exp (double);
double ves_icall_System_Math_Floor (double);
double ves_icall_System_Math_Log (double);
double ves_icall_System_Math_Log10 (double);
double ves_icall_System_Math_Pow (double,double);
double ves_icall_System_Math_Sin (double);
double ves_icall_System_Math_Sinh (double);
double ves_icall_System_Math_Sqrt (double);
double ves_icall_System_Math_Tan (double);
double ves_icall_System_Math_Tanh (double);
double ves_icall_System_Math_FusedMultiplyAdd (double,double,double);
double ves_icall_System_Math_Log2 (double);
double ves_icall_System_Math_ModF (double,int);
float ves_icall_System_MathF_Acos (float);
float ves_icall_System_MathF_Acosh (float);
float ves_icall_System_MathF_Asin (float);
float ves_icall_System_MathF_Asinh (float);
float ves_icall_System_MathF_Atan (float);
float ves_icall_System_MathF_Atan2 (float,float);
float ves_icall_System_MathF_Atanh (float);
float ves_icall_System_MathF_Cbrt (float);
float ves_icall_System_MathF_Ceiling (float);
float ves_icall_System_MathF_Cos (float);
float ves_icall_System_MathF_Cosh (float);
float ves_icall_System_MathF_Exp (float);
float ves_icall_System_MathF_Floor (float);
float ves_icall_System_MathF_Log (float);
float ves_icall_System_MathF_Log10 (float);
float ves_icall_System_MathF_Pow (float,float);
float ves_icall_System_MathF_Sin (float);
float ves_icall_System_MathF_Sinh (float);
float ves_icall_System_MathF_Sqrt (float);
float ves_icall_System_MathF_Tan (float);
float ves_icall_System_MathF_Tanh (float);
float ves_icall_System_MathF_FusedMultiplyAdd (float,float,float);
float ves_icall_System_MathF_Log2 (float);
float ves_icall_System_MathF_ModF (float,int);
int ves_icall_RuntimeMethodHandle_GetFunctionPointer_raw (int,int);
void ves_icall_RuntimeMethodHandle_ReboxFromNullable_raw (int,int,int);
void ves_icall_RuntimeMethodHandle_ReboxToNullable_raw (int,int,int,int);
int ves_icall_RuntimeType_GetCorrespondingInflatedMethod_raw (int,int,int);
void ves_icall_RuntimeType_make_array_type_raw (int,int,int,int);
void ves_icall_RuntimeType_make_byref_type_raw (int,int,int);
void ves_icall_RuntimeType_make_pointer_type_raw (int,int,int);
void ves_icall_RuntimeType_MakeGenericType_raw (int,int,int,int);
int ves_icall_RuntimeType_GetMethodsByName_native_raw (int,int,int,int,int);
int ves_icall_RuntimeType_GetPropertiesByName_native_raw (int,int,int,int,int);
int ves_icall_RuntimeType_GetConstructors_native_raw (int,int,int);
int ves_icall_System_RuntimeType_CreateInstanceInternal_raw (int,int);
void ves_icall_RuntimeType_GetDeclaringMethod_raw (int,int,int);
void ves_icall_System_RuntimeType_getFullName_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetGenericArgumentsInternal_raw (int,int,int,int);
int ves_icall_RuntimeType_GetGenericParameterPosition (int);
int ves_icall_RuntimeType_GetEvents_native_raw (int,int,int,int);
int ves_icall_RuntimeType_GetFields_native_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetInterfaces_raw (int,int,int);
int ves_icall_RuntimeType_GetNestedTypes_native_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetDeclaringType_raw (int,int,int);
void ves_icall_RuntimeType_GetName_raw (int,int,int);
void ves_icall_RuntimeType_GetNamespace_raw (int,int,int);
int ves_icall_RuntimeType_FunctionPointerReturnAndParameterTypes_raw (int,int);
int ves_icall_RuntimeTypeHandle_GetAttributes (int);
int ves_icall_RuntimeTypeHandle_GetMetadataToken_raw (int,int);
void ves_icall_RuntimeTypeHandle_GetGenericTypeDefinition_impl_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_GetCorElementType (int);
int ves_icall_RuntimeTypeHandle_HasInstantiation (int);
int ves_icall_RuntimeTypeHandle_IsInstanceOfType_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_HasReferences_raw (int,int);
int ves_icall_RuntimeTypeHandle_GetArrayRank_raw (int,int);
void ves_icall_RuntimeTypeHandle_GetAssembly_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetElementType_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetModule_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetBaseType_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_type_is_assignable_from_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_IsGenericTypeDefinition (int);
int ves_icall_RuntimeTypeHandle_GetGenericParameterInfo_raw (int,int);
int ves_icall_RuntimeTypeHandle_is_subclass_of_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_IsByRefLike_raw (int,int);
void ves_icall_System_RuntimeTypeHandle_internal_from_name_raw (int,int,int,int,int,int);
int ves_icall_System_String_FastAllocateString_raw (int,int);
int ves_icall_System_String_InternalIsInterned_raw (int,int);
int ves_icall_System_String_InternalIntern_raw (int,int);
int ves_icall_System_Type_internal_from_handle_raw (int,int);
int ves_icall_System_ValueType_InternalGetHashCode_raw (int,int,int);
int ves_icall_System_ValueType_Equals_raw (int,int,int,int);
int ves_icall_System_Threading_Interlocked_CompareExchange_Int (int,int,int);
void ves_icall_System_Threading_Interlocked_CompareExchange_Object (int,int,int,int);
int ves_icall_System_Threading_Interlocked_Decrement_Int (int);
int64_t ves_icall_System_Threading_Interlocked_Decrement_Long (int);
int ves_icall_System_Threading_Interlocked_Increment_Int (int);
int64_t ves_icall_System_Threading_Interlocked_Increment_Long (int);
int ves_icall_System_Threading_Interlocked_Exchange_Int (int,int);
void ves_icall_System_Threading_Interlocked_Exchange_Object (int,int,int);
int64_t ves_icall_System_Threading_Interlocked_CompareExchange_Long (int,int64_t,int64_t);
int64_t ves_icall_System_Threading_Interlocked_Exchange_Long (int,int64_t);
int64_t ves_icall_System_Threading_Interlocked_Read_Long (int);
int ves_icall_System_Threading_Interlocked_Add_Int (int,int);
int64_t ves_icall_System_Threading_Interlocked_Add_Long (int,int64_t);
void ves_icall_System_Threading_Monitor_Monitor_Enter_raw (int,int);
void mono_monitor_exit_icall_raw (int,int);
void ves_icall_System_Threading_Monitor_Monitor_pulse_raw (int,int);
void ves_icall_System_Threading_Monitor_Monitor_pulse_all_raw (int,int);
int ves_icall_System_Threading_Monitor_Monitor_wait_raw (int,int,int,int);
void ves_icall_System_Threading_Monitor_Monitor_try_enter_with_atomic_var_raw (int,int,int,int,int);
int64_t ves_icall_System_Threading_Monitor_Monitor_get_lock_contention_count ();
void ves_icall_System_Threading_Thread_InitInternal_raw (int,int);
int ves_icall_System_Threading_Thread_GetCurrentThread ();
void ves_icall_System_Threading_InternalThread_Thread_free_internal_raw (int,int);
int ves_icall_System_Threading_Thread_GetState_raw (int,int);
void ves_icall_System_Threading_Thread_SetState_raw (int,int,int);
void ves_icall_System_Threading_Thread_ClrState_raw (int,int,int);
void ves_icall_System_Threading_Thread_SetName_icall_raw (int,int,int,int);
int ves_icall_System_Threading_Thread_YieldInternal ();
void ves_icall_System_Threading_Thread_SetPriority_raw (int,int,int);
void ves_icall_System_Runtime_Loader_AssemblyLoadContext_PrepareForAssemblyLoadContextRelease_raw (int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_GetLoadContextForAssembly_raw (int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFile_raw (int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalInitializeNativeALC_raw (int,int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFromStream_raw (int,int,int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalGetLoadedAssemblies_raw (int);
int ves_icall_System_GCHandle_InternalAlloc_raw (int,int,int);
void ves_icall_System_GCHandle_InternalFree_raw (int,int);
int ves_icall_System_GCHandle_InternalGet_raw (int,int);
void ves_icall_System_GCHandle_InternalSet_raw (int,int,int);
int ves_icall_System_Runtime_InteropServices_Marshal_GetLastPInvokeError ();
void ves_icall_System_Runtime_InteropServices_Marshal_SetLastPInvokeError (int);
void ves_icall_System_Runtime_InteropServices_Marshal_StructureToPtr_raw (int,int,int,int);
int ves_icall_System_Runtime_InteropServices_NativeLibrary_LoadByName_raw (int,int,int,int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalGetHashCode_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetObjectValue_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetUninitializedObjectInternal_raw (int,int);
void ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InitializeArray_raw (int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetSpanDataFrom_raw (int,int,int,int);
void ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_RunClassConstructor_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_SufficientExecutionStack ();
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalBox_raw (int,int,int);
int ves_icall_System_Reflection_Assembly_GetExecutingAssembly_raw (int,int);
int ves_icall_System_Reflection_Assembly_GetEntryAssembly_raw (int);
int ves_icall_System_Reflection_Assembly_InternalLoad_raw (int,int,int,int);
int ves_icall_System_Reflection_Assembly_InternalGetType_raw (int,int,int,int,int,int);
int ves_icall_System_Reflection_AssemblyName_GetNativeName (int);
int ves_icall_MonoCustomAttrs_GetCustomAttributesInternal_raw (int,int,int,int);
int ves_icall_MonoCustomAttrs_GetCustomAttributesDataInternal_raw (int,int);
int ves_icall_MonoCustomAttrs_IsDefinedInternal_raw (int,int,int);
int ves_icall_System_Reflection_FieldInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_System_Reflection_FieldInfo_get_marshal_info_raw (int,int);
int ves_icall_System_Reflection_LoaderAllocatorScout_Destroy (int);
void ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceNames_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetExportedTypes_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetInfo_raw (int,int,int,int);
int ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceInternal_raw (int,int,int,int,int);
void ves_icall_System_Reflection_Assembly_GetManifestModuleInternal_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetModulesInternal_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeCustomAttributeData_ResolveArgumentsInternal_raw (int,int,int,int,int,int,int);
void ves_icall_RuntimeEventInfo_get_event_info_raw (int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_EventInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_RuntimeFieldInfo_ResolveType_raw (int,int);
int ves_icall_RuntimeFieldInfo_GetParentType_raw (int,int,int);
int ves_icall_RuntimeFieldInfo_GetFieldOffset_raw (int,int);
int ves_icall_RuntimeFieldInfo_GetValueInternal_raw (int,int,int);
void ves_icall_RuntimeFieldInfo_SetValueInternal_raw (int,int,int,int);
int ves_icall_RuntimeFieldInfo_GetRawConstantValue_raw (int,int);
int ves_icall_reflection_get_token_raw (int,int);
void ves_icall_get_method_info_raw (int,int,int);
int ves_icall_get_method_attributes (int);
int ves_icall_System_Reflection_MonoMethodInfo_get_parameter_info_raw (int,int,int);
int ves_icall_System_MonoMethodInfo_get_retval_marshal_raw (int,int);
int ves_icall_System_Reflection_RuntimeMethodInfo_GetMethodFromHandleInternalType_native_raw (int,int,int,int);
int ves_icall_RuntimeMethodInfo_get_name_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_base_method_raw (int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_InternalInvoke_raw (int,int,int,int,int);
void ves_icall_RuntimeMethodInfo_GetPInvoke_raw (int,int,int,int,int);
int ves_icall_RuntimeMethodInfo_MakeGenericMethod_impl_raw (int,int,int);
int ves_icall_RuntimeMethodInfo_GetGenericArguments_raw (int,int);
int ves_icall_RuntimeMethodInfo_GetGenericMethodDefinition_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_IsGenericMethodDefinition_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_IsGenericMethod_raw (int,int);
void ves_icall_InvokeClassConstructor_raw (int,int);
int ves_icall_InternalInvoke_raw (int,int,int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
void ves_icall_System_Reflection_RuntimeModule_GetGuidInternal_raw (int,int,int);
int ves_icall_System_Reflection_RuntimeModule_ResolveMethodToken_raw (int,int,int,int,int,int);
int ves_icall_RuntimeParameterInfo_GetTypeModifiers_raw (int,int,int,int,int,int);
void ves_icall_RuntimePropertyInfo_get_property_info_raw (int,int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_RuntimePropertyInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_CustomAttributeBuilder_GetBlob_raw (int,int,int,int,int,int,int,int);
void ves_icall_DynamicMethod_create_dynamic_method_raw (int,int,int,int,int);
void ves_icall_AssemblyBuilder_basic_init_raw (int,int);
void ves_icall_AssemblyBuilder_UpdateNativeCustomAttributes_raw (int,int);
void ves_icall_ModuleBuilder_basic_init_raw (int,int);
void ves_icall_ModuleBuilder_set_wrappers_type_raw (int,int,int);
int ves_icall_ModuleBuilder_getUSIndex_raw (int,int,int);
int ves_icall_ModuleBuilder_getToken_raw (int,int,int,int);
int ves_icall_ModuleBuilder_getMethodToken_raw (int,int,int,int);
void ves_icall_ModuleBuilder_RegisterToken_raw (int,int,int,int);
int ves_icall_TypeBuilder_create_runtime_class_raw (int,int);
int ves_icall_System_IO_Stream_HasOverriddenBeginEndRead_raw (int,int);
int ves_icall_System_IO_Stream_HasOverriddenBeginEndWrite_raw (int,int);
int ves_icall_System_Diagnostics_Debugger_IsAttached_internal ();
int ves_icall_System_Diagnostics_Debugger_IsLogging ();
void ves_icall_System_Diagnostics_Debugger_Log (int,int,int);
int ves_icall_System_Diagnostics_StackFrame_GetFrameInfo (int,int,int,int,int,int,int,int);
void ves_icall_System_Diagnostics_StackTrace_GetTrace (int,int,int,int);
int ves_icall_Mono_RuntimeClassHandle_GetTypeFromClass (int);
void ves_icall_Mono_RuntimeGPtrArrayHandle_GPtrArrayFree (int);
int ves_icall_Mono_SafeStringMarshal_StringToUtf8 (int);
void ves_icall_Mono_SafeStringMarshal_GFree (int);
static void *corlib_icall_funcs [] = {
// token 220,
ves_icall_System_Array_InternalCreate,
// token 233,
ves_icall_System_Array_GetCorElementTypeOfElementTypeInternal,
// token 234,
ves_icall_System_Array_IsValueOfElementTypeInternal,
// token 235,
ves_icall_System_Array_CanChangePrimitive,
// token 236,
ves_icall_System_Array_FastCopy,
// token 237,
ves_icall_System_Array_GetLengthInternal_raw,
// token 238,
ves_icall_System_Array_GetLowerBoundInternal_raw,
// token 239,
ves_icall_System_Array_GetGenericValue_icall,
// token 240,
ves_icall_System_Array_GetValueImpl_raw,
// token 241,
ves_icall_System_Array_SetGenericValue_icall,
// token 244,
ves_icall_System_Array_SetValueImpl_raw,
// token 245,
ves_icall_System_Array_InitializeInternal_raw,
// token 246,
ves_icall_System_Array_SetValueRelaxedImpl_raw,
// token 423,
ves_icall_System_Runtime_RuntimeImports_ZeroMemory,
// token 424,
ves_icall_System_Runtime_RuntimeImports_Memmove,
// token 425,
ves_icall_System_Buffer_BulkMoveWithWriteBarrier,
// token 453,
ves_icall_System_Delegate_AllocDelegateLike_internal_raw,
// token 454,
ves_icall_System_Delegate_CreateDelegate_internal_raw,
// token 455,
ves_icall_System_Delegate_GetVirtualMethod_internal_raw,
// token 482,
ves_icall_System_Enum_GetEnumValuesAndNames_raw,
// token 483,
ves_icall_System_Enum_InternalGetCorElementType,
// token 484,
ves_icall_System_Enum_InternalGetUnderlyingType_raw,
// token 601,
ves_icall_System_Environment_get_ProcessorCount,
// token 602,
ves_icall_System_Environment_get_TickCount,
// token 603,
ves_icall_System_Environment_get_TickCount64,
// token 606,
ves_icall_System_Environment_FailFast_raw,
// token 644,
ves_icall_System_GC_GetCollectionCount,
// token 645,
ves_icall_System_GC_GetMaxGeneration,
// token 646,
ves_icall_System_GC_register_ephemeron_array_raw,
// token 647,
ves_icall_System_GC_get_ephemeron_tombstone_raw,
// token 648,
ves_icall_System_GC_GetTotalAllocatedBytes_raw,
// token 652,
ves_icall_System_GC_SuppressFinalize_raw,
// token 654,
ves_icall_System_GC_ReRegisterForFinalize_raw,
// token 656,
ves_icall_System_GC_GetGCMemoryInfo,
// token 658,
ves_icall_System_GC_AllocPinnedArray_raw,
// token 664,
ves_icall_System_Object_MemberwiseClone_raw,
// token 672,
ves_icall_System_Math_Acos,
// token 673,
ves_icall_System_Math_Acosh,
// token 674,
ves_icall_System_Math_Asin,
// token 675,
ves_icall_System_Math_Asinh,
// token 676,
ves_icall_System_Math_Atan,
// token 677,
ves_icall_System_Math_Atan2,
// token 678,
ves_icall_System_Math_Atanh,
// token 679,
ves_icall_System_Math_Cbrt,
// token 680,
ves_icall_System_Math_Ceiling,
// token 681,
ves_icall_System_Math_Cos,
// token 682,
ves_icall_System_Math_Cosh,
// token 683,
ves_icall_System_Math_Exp,
// token 684,
ves_icall_System_Math_Floor,
// token 685,
ves_icall_System_Math_Log,
// token 686,
ves_icall_System_Math_Log10,
// token 687,
ves_icall_System_Math_Pow,
// token 688,
ves_icall_System_Math_Sin,
// token 690,
ves_icall_System_Math_Sinh,
// token 691,
ves_icall_System_Math_Sqrt,
// token 692,
ves_icall_System_Math_Tan,
// token 693,
ves_icall_System_Math_Tanh,
// token 694,
ves_icall_System_Math_FusedMultiplyAdd,
// token 695,
ves_icall_System_Math_Log2,
// token 696,
ves_icall_System_Math_ModF,
// token 793,
ves_icall_System_MathF_Acos,
// token 794,
ves_icall_System_MathF_Acosh,
// token 795,
ves_icall_System_MathF_Asin,
// token 796,
ves_icall_System_MathF_Asinh,
// token 797,
ves_icall_System_MathF_Atan,
// token 798,
ves_icall_System_MathF_Atan2,
// token 799,
ves_icall_System_MathF_Atanh,
// token 800,
ves_icall_System_MathF_Cbrt,
// token 801,
ves_icall_System_MathF_Ceiling,
// token 802,
ves_icall_System_MathF_Cos,
// token 803,
ves_icall_System_MathF_Cosh,
// token 804,
ves_icall_System_MathF_Exp,
// token 805,
ves_icall_System_MathF_Floor,
// token 806,
ves_icall_System_MathF_Log,
// token 807,
ves_icall_System_MathF_Log10,
// token 808,
ves_icall_System_MathF_Pow,
// token 809,
ves_icall_System_MathF_Sin,
// token 811,
ves_icall_System_MathF_Sinh,
// token 812,
ves_icall_System_MathF_Sqrt,
// token 813,
ves_icall_System_MathF_Tan,
// token 814,
ves_icall_System_MathF_Tanh,
// token 815,
ves_icall_System_MathF_FusedMultiplyAdd,
// token 816,
ves_icall_System_MathF_Log2,
// token 817,
ves_icall_System_MathF_ModF,
// token 879,
ves_icall_RuntimeMethodHandle_GetFunctionPointer_raw,
// token 888,
ves_icall_RuntimeMethodHandle_ReboxFromNullable_raw,
// token 889,
ves_icall_RuntimeMethodHandle_ReboxToNullable_raw,
// token 960,
ves_icall_RuntimeType_GetCorrespondingInflatedMethod_raw,
// token 967,
ves_icall_RuntimeType_make_array_type_raw,
// token 970,
ves_icall_RuntimeType_make_byref_type_raw,
// token 972,
ves_icall_RuntimeType_make_pointer_type_raw,
// token 977,
ves_icall_RuntimeType_MakeGenericType_raw,
// token 978,
ves_icall_RuntimeType_GetMethodsByName_native_raw,
// token 980,
ves_icall_RuntimeType_GetPropertiesByName_native_raw,
// token 981,
ves_icall_RuntimeType_GetConstructors_native_raw,
// token 985,
ves_icall_System_RuntimeType_CreateInstanceInternal_raw,
// token 986,
ves_icall_RuntimeType_GetDeclaringMethod_raw,
// token 988,
ves_icall_System_RuntimeType_getFullName_raw,
// token 989,
ves_icall_RuntimeType_GetGenericArgumentsInternal_raw,
// token 992,
ves_icall_RuntimeType_GetGenericParameterPosition,
// token 993,
ves_icall_RuntimeType_GetEvents_native_raw,
// token 994,
ves_icall_RuntimeType_GetFields_native_raw,
// token 997,
ves_icall_RuntimeType_GetInterfaces_raw,
// token 999,
ves_icall_RuntimeType_GetNestedTypes_native_raw,
// token 1002,
ves_icall_RuntimeType_GetDeclaringType_raw,
// token 1004,
ves_icall_RuntimeType_GetName_raw,
// token 1006,
ves_icall_RuntimeType_GetNamespace_raw,
// token 1015,
ves_icall_RuntimeType_FunctionPointerReturnAndParameterTypes_raw,
// token 1084,
ves_icall_RuntimeTypeHandle_GetAttributes,
// token 1086,
ves_icall_RuntimeTypeHandle_GetMetadataToken_raw,
// token 1088,
ves_icall_RuntimeTypeHandle_GetGenericTypeDefinition_impl_raw,
// token 1098,
ves_icall_RuntimeTypeHandle_GetCorElementType,
// token 1099,
ves_icall_RuntimeTypeHandle_HasInstantiation,
// token 1100,
ves_icall_RuntimeTypeHandle_IsInstanceOfType_raw,
// token 1102,
ves_icall_RuntimeTypeHandle_HasReferences_raw,
// token 1108,
ves_icall_RuntimeTypeHandle_GetArrayRank_raw,
// token 1109,
ves_icall_RuntimeTypeHandle_GetAssembly_raw,
// token 1110,
ves_icall_RuntimeTypeHandle_GetElementType_raw,
// token 1111,
ves_icall_RuntimeTypeHandle_GetModule_raw,
// token 1112,
ves_icall_RuntimeTypeHandle_GetBaseType_raw,
// token 1120,
ves_icall_RuntimeTypeHandle_type_is_assignable_from_raw,
// token 1121,
ves_icall_RuntimeTypeHandle_IsGenericTypeDefinition,
// token 1122,
ves_icall_RuntimeTypeHandle_GetGenericParameterInfo_raw,
// token 1126,
ves_icall_RuntimeTypeHandle_is_subclass_of_raw,
// token 1127,
ves_icall_RuntimeTypeHandle_IsByRefLike_raw,
// token 1129,
ves_icall_System_RuntimeTypeHandle_internal_from_name_raw,
// token 1133,
ves_icall_System_String_FastAllocateString_raw,
// token 1134,
ves_icall_System_String_InternalIsInterned_raw,
// token 1135,
ves_icall_System_String_InternalIntern_raw,
// token 1432,
ves_icall_System_Type_internal_from_handle_raw,
// token 1622,
ves_icall_System_ValueType_InternalGetHashCode_raw,
// token 1623,
ves_icall_System_ValueType_Equals_raw,
// token 10091,
ves_icall_System_Threading_Interlocked_CompareExchange_Int,
// token 10092,
ves_icall_System_Threading_Interlocked_CompareExchange_Object,
// token 10094,
ves_icall_System_Threading_Interlocked_Decrement_Int,
// token 10095,
ves_icall_System_Threading_Interlocked_Decrement_Long,
// token 10096,
ves_icall_System_Threading_Interlocked_Increment_Int,
// token 10097,
ves_icall_System_Threading_Interlocked_Increment_Long,
// token 10098,
ves_icall_System_Threading_Interlocked_Exchange_Int,
// token 10099,
ves_icall_System_Threading_Interlocked_Exchange_Object,
// token 10101,
ves_icall_System_Threading_Interlocked_CompareExchange_Long,
// token 10102,
ves_icall_System_Threading_Interlocked_Exchange_Long,
// token 10103,
ves_icall_System_Threading_Interlocked_Read_Long,
// token 10104,
ves_icall_System_Threading_Interlocked_Add_Int,
// token 10105,
ves_icall_System_Threading_Interlocked_Add_Long,
// token 10123,
ves_icall_System_Threading_Monitor_Monitor_Enter_raw,
// token 10125,
mono_monitor_exit_icall_raw,
// token 10133,
ves_icall_System_Threading_Monitor_Monitor_pulse_raw,
// token 10135,
ves_icall_System_Threading_Monitor_Monitor_pulse_all_raw,
// token 10137,
ves_icall_System_Threading_Monitor_Monitor_wait_raw,
// token 10139,
ves_icall_System_Threading_Monitor_Monitor_try_enter_with_atomic_var_raw,
// token 10142,
ves_icall_System_Threading_Monitor_Monitor_get_lock_contention_count,
// token 10193,
ves_icall_System_Threading_Thread_InitInternal_raw,
// token 10194,
ves_icall_System_Threading_Thread_GetCurrentThread,
// token 10196,
ves_icall_System_Threading_InternalThread_Thread_free_internal_raw,
// token 10197,
ves_icall_System_Threading_Thread_GetState_raw,
// token 10198,
ves_icall_System_Threading_Thread_SetState_raw,
// token 10199,
ves_icall_System_Threading_Thread_ClrState_raw,
// token 10200,
ves_icall_System_Threading_Thread_SetName_icall_raw,
// token 10202,
ves_icall_System_Threading_Thread_YieldInternal,
// token 10204,
ves_icall_System_Threading_Thread_SetPriority_raw,
// token 11407,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_PrepareForAssemblyLoadContextRelease_raw,
// token 11411,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_GetLoadContextForAssembly_raw,
// token 11413,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFile_raw,
// token 11414,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalInitializeNativeALC_raw,
// token 11415,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFromStream_raw,
// token 11416,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalGetLoadedAssemblies_raw,
// token 11858,
ves_icall_System_GCHandle_InternalAlloc_raw,
// token 11859,
ves_icall_System_GCHandle_InternalFree_raw,
// token 11860,
ves_icall_System_GCHandle_InternalGet_raw,
// token 11861,
ves_icall_System_GCHandle_InternalSet_raw,
// token 11879,
ves_icall_System_Runtime_InteropServices_Marshal_GetLastPInvokeError,
// token 11880,
ves_icall_System_Runtime_InteropServices_Marshal_SetLastPInvokeError,
// token 11881,
ves_icall_System_Runtime_InteropServices_Marshal_StructureToPtr_raw,
// token 11927,
ves_icall_System_Runtime_InteropServices_NativeLibrary_LoadByName_raw,
// token 11995,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalGetHashCode_raw,
// token 11998,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetObjectValue_raw,
// token 12007,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetUninitializedObjectInternal_raw,
// token 12008,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InitializeArray_raw,
// token 12009,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetSpanDataFrom_raw,
// token 12010,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_RunClassConstructor_raw,
// token 12011,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_SufficientExecutionStack,
// token 12012,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalBox_raw,
// token 12359,
ves_icall_System_Reflection_Assembly_GetExecutingAssembly_raw,
// token 12360,
ves_icall_System_Reflection_Assembly_GetEntryAssembly_raw,
// token 12365,
ves_icall_System_Reflection_Assembly_InternalLoad_raw,
// token 12366,
ves_icall_System_Reflection_Assembly_InternalGetType_raw,
// token 12401,
ves_icall_System_Reflection_AssemblyName_GetNativeName,
// token 12444,
ves_icall_MonoCustomAttrs_GetCustomAttributesInternal_raw,
// token 12451,
ves_icall_MonoCustomAttrs_GetCustomAttributesDataInternal_raw,
// token 12458,
ves_icall_MonoCustomAttrs_IsDefinedInternal_raw,
// token 12469,
ves_icall_System_Reflection_FieldInfo_internal_from_handle_type_raw,
// token 12473,
ves_icall_System_Reflection_FieldInfo_get_marshal_info_raw,
// token 12499,
ves_icall_System_Reflection_LoaderAllocatorScout_Destroy,
// token 12582,
ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceNames_raw,
// token 12584,
ves_icall_System_Reflection_RuntimeAssembly_GetExportedTypes_raw,
// token 12595,
ves_icall_System_Reflection_RuntimeAssembly_GetInfo_raw,
// token 12597,
ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceInternal_raw,
// token 12598,
ves_icall_System_Reflection_Assembly_GetManifestModuleInternal_raw,
// token 12599,
ves_icall_System_Reflection_RuntimeAssembly_GetModulesInternal_raw,
// token 12606,
ves_icall_System_Reflection_RuntimeCustomAttributeData_ResolveArgumentsInternal_raw,
// token 12621,
ves_icall_RuntimeEventInfo_get_event_info_raw,
// token 12641,
ves_icall_reflection_get_token_raw,
// token 12642,
ves_icall_System_Reflection_EventInfo_internal_from_handle_type_raw,
// token 12650,
ves_icall_RuntimeFieldInfo_ResolveType_raw,
// token 12652,
ves_icall_RuntimeFieldInfo_GetParentType_raw,
// token 12659,
ves_icall_RuntimeFieldInfo_GetFieldOffset_raw,
// token 12660,
ves_icall_RuntimeFieldInfo_GetValueInternal_raw,
// token 12663,
ves_icall_RuntimeFieldInfo_SetValueInternal_raw,
// token 12665,
ves_icall_RuntimeFieldInfo_GetRawConstantValue_raw,
// token 12670,
ves_icall_reflection_get_token_raw,
// token 12676,
ves_icall_get_method_info_raw,
// token 12677,
ves_icall_get_method_attributes,
// token 12684,
ves_icall_System_Reflection_MonoMethodInfo_get_parameter_info_raw,
// token 12686,
ves_icall_System_MonoMethodInfo_get_retval_marshal_raw,
// token 12698,
ves_icall_System_Reflection_RuntimeMethodInfo_GetMethodFromHandleInternalType_native_raw,
// token 12701,
ves_icall_RuntimeMethodInfo_get_name_raw,
// token 12702,
ves_icall_RuntimeMethodInfo_get_base_method_raw,
// token 12703,
ves_icall_reflection_get_token_raw,
// token 12714,
ves_icall_InternalInvoke_raw,
// token 12724,
ves_icall_RuntimeMethodInfo_GetPInvoke_raw,
// token 12730,
ves_icall_RuntimeMethodInfo_MakeGenericMethod_impl_raw,
// token 12731,
ves_icall_RuntimeMethodInfo_GetGenericArguments_raw,
// token 12732,
ves_icall_RuntimeMethodInfo_GetGenericMethodDefinition_raw,
// token 12734,
ves_icall_RuntimeMethodInfo_get_IsGenericMethodDefinition_raw,
// token 12735,
ves_icall_RuntimeMethodInfo_get_IsGenericMethod_raw,
// token 12752,
ves_icall_InvokeClassConstructor_raw,
// token 12754,
ves_icall_InternalInvoke_raw,
// token 12769,
ves_icall_reflection_get_token_raw,
// token 12791,
ves_icall_System_Reflection_RuntimeModule_GetGuidInternal_raw,
// token 12792,
ves_icall_System_Reflection_RuntimeModule_ResolveMethodToken_raw,
// token 12817,
ves_icall_RuntimeParameterInfo_GetTypeModifiers_raw,
// token 12822,
ves_icall_RuntimePropertyInfo_get_property_info_raw,
// token 12852,
ves_icall_reflection_get_token_raw,
// token 12853,
ves_icall_System_Reflection_RuntimePropertyInfo_internal_from_handle_type_raw,
// token 13482,
ves_icall_CustomAttributeBuilder_GetBlob_raw,
// token 13496,
ves_icall_DynamicMethod_create_dynamic_method_raw,
// token 13585,
ves_icall_AssemblyBuilder_basic_init_raw,
// token 13586,
ves_icall_AssemblyBuilder_UpdateNativeCustomAttributes_raw,
// token 13808,
ves_icall_ModuleBuilder_basic_init_raw,
// token 13809,
ves_icall_ModuleBuilder_set_wrappers_type_raw,
// token 13816,
ves_icall_ModuleBuilder_getUSIndex_raw,
// token 13817,
ves_icall_ModuleBuilder_getToken_raw,
// token 13818,
ves_icall_ModuleBuilder_getMethodToken_raw,
// token 13824,
ves_icall_ModuleBuilder_RegisterToken_raw,
// token 13894,
ves_icall_TypeBuilder_create_runtime_class_raw,
// token 14432,
ves_icall_System_IO_Stream_HasOverriddenBeginEndRead_raw,
// token 14433,
ves_icall_System_IO_Stream_HasOverriddenBeginEndWrite_raw,
// token 14863,
ves_icall_System_Diagnostics_Debugger_IsAttached_internal,
// token 14864,
ves_icall_System_Diagnostics_Debugger_IsLogging,
// token 14865,
ves_icall_System_Diagnostics_Debugger_Log,
// token 14871,
ves_icall_System_Diagnostics_StackFrame_GetFrameInfo,
// token 14881,
ves_icall_System_Diagnostics_StackTrace_GetTrace,
// token 15895,
ves_icall_Mono_RuntimeClassHandle_GetTypeFromClass,
// token 15916,
ves_icall_Mono_RuntimeGPtrArrayHandle_GPtrArrayFree,
// token 15918,
ves_icall_Mono_SafeStringMarshal_StringToUtf8,
// token 15920,
ves_icall_Mono_SafeStringMarshal_GFree,
};
static uint8_t corlib_icall_flags [] = {
0,
0,
0,
0,
0,
4,
4,
0,
4,
0,
4,
4,
4,
0,
0,
0,
4,
4,
4,
4,
0,
4,
0,
0,
0,
4,
0,
0,
4,
4,
4,
4,
4,
0,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
4,
4,
4,
4,
4,
4,
0,
4,
0,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
0,
};
