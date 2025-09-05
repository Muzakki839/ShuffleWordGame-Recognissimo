using UnityEngine;
using Recognissimo.Components; // Pastikan namespace ini ada
using Recognissimo.Core;       // Namespace untuk SamplesReadyEventArgs
using Recognissimo;

public class SpeechManager : MonoBehaviour
{
    // 1. Buat variabel untuk menampung referensi ke MicrophoneSpeechSource.
    // [SerializeField] membuatnya bisa di-drag & drop dari Inspector Unity.
    [SerializeField]
    private MicrophoneSpeechSource speechSource;

    private void Awake()
    {
        // Alternatif jika komponen ada di GameObject yang sama:
        // speechSource = GetComponent<MicrophoneSpeechSource>();

        if (speechSource == null)
        {
            Debug.LogError("MicrophoneSpeechSource tidak ditemukan!");
            enabled = false; // Matikan skrip ini jika referensi tidak ada.
        }
    }

    // 2. Gunakan OnEnable dan OnDisable untuk berlangganan (subscribe) dan berhenti berlangganan (unsubscribe) event.
    // Ini adalah praktik terbaik untuk menghindari memory leak.
    private void OnEnable()
    {
        // Berlangganan event 'SamplesReady' untuk menerima data audio.
        speechSource.SamplesReady += OnSamplesReady;
        // Berlangganan event untuk menangani error jika terjadi.
        speechSource.RuntimeFailure += OnRuntimeFailure;
    }

    private void OnDisable()
    {
        // Selalu berhenti berlangganan saat skrip tidak aktif atau dihancurkan.
        speechSource.SamplesReady -= OnSamplesReady;
        speechSource.RuntimeFailure -= OnRuntimeFailure;
    }

    // Fungsi ini akan dipanggil setiap kali MicrophoneSpeechSource memiliki data audio baru.
    private void OnSamplesReady(object sender, SamplesReadyEventArgs args)
    {
        // 'args.Samples' adalah array float[] yang berisi data audio PCM.
        // 'args.Count' adalah jumlah sampel yang valid di dalam array.
        Debug.Log($"🎤 Menerima {args} sampel audio.");

        // Di sinilah Anda akan memproses data audio tersebut,
        // misalnya mengirimkannya ke library speech-to-text lain.
        // Contoh: ProcessAudio(args.Samples, args.Count);
    }

    private void OnRuntimeFailure(object sender, RuntimeFailureEventArgs args)
    {
        Debug.LogError($"Terjadi error saat runtime: {args.Exception.Message}");
    }

    // 3. Buat fungsi publik untuk mengontrol perekaman dari skrip lain atau dari UI Button.
    public void StartRecording()
    {
        if (speechSource != null && !speechSource.IsRecording)
        {
            Debug.Log("Memulai perekaman...");
            speechSource.StartProducing();
        }
    }

    public void StopRecording()
    {
        if (speechSource != null && speechSource.IsRecording)
        {
            Debug.Log("Menghentikan perekaman...");
            speechSource.StopProducing();
        }
    }

    // Contoh penggunaan dalam game
    /*void Update()
    {
        // Tekan tombol 'S' untuk mulai merekam
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartRecording();
        }

        // Tekan tombol 'X' untuk berhenti merekam
        if (Input.GetKeyDown(KeyCode.X))
        {
            StopRecording();
        }
    }*/
}