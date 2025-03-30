import SwiftUI

struct Mobil: Identifiable {
    var id = UUID()
    var nama: String
    var merek: String
    var hargaPerHari: Double
}

class PenyewaanMobilViewModel: ObservableObject {
    @Published var mobilList: [Mobil] = []
    
    func tambahMobil(nama: String, merek: String, harga: Double) {
        let mobilBaru = Mobil(nama: nama, merek: merek, hargaPerHari: harga)
        mobilList.append(mobilBaru)
    }
    
    func hapusMobil(at offsets: IndexSet) {
        mobilList.remove(atOffsets: offsets)
    }
    
    func perbaruiMobil(mobil: Mobil, namaBaru: String, merekBaru: String, hargaBaru: Double) {
        if let index = mobilList.firstIndex(where: { $0.id == mobil.id }) {
            mobilList[index] = Mobil(id: mobil.id, nama: namaBaru, merek: merekBaru, hargaPerHari: hargaBaru)
        }
    }
}

struct KontenView: View {
    @StateObject private var viewModel = PenyewaanMobilViewModel()
    @State private var tampilTambahMobil = false
    
    var body: some View {
        NavigationView {
            List {
                ForEach(viewModel.mobilList) { mobil in
                    HStack {
                        VStack(alignment: .leading) {
                            Text(mobil.nama).font(.headline)
                            Text(mobil.merek).font(.subheadline)
                            Text("Rp\(mobil.hargaPerHari, specifier: "%.2f") per hari")
                        }
                        Spacer()
                        Button("Edit") {
                            tampilTambahMobil.toggle()
                        }
                    }
                }
                .onDelete(perform: viewModel.hapusMobil)
            }
            .navigationTitle("Penyewaan Mobil")
            .toolbar {
                ToolbarItem(placement: .automatic) {
                    Button(action: { tampilTambahMobil.toggle() }) {
                        Image(systemName: "plus")
                    }
                }
            }
            .sheet(isPresented: $tampilTambahMobil) {
                TambahMobilView(viewModel: viewModel)
            }
        }
    }
}

struct TambahMobilView: View {
    @Environment(\.presentationMode) var presentationMode
    @ObservedObject var viewModel: PenyewaanMobilViewModel
    @State private var nama = ""
    @State private var merek = ""
    @State private var harga = ""
    
    var body: some View {
        NavigationView {
            Form {
                TextField("Nama Mobil", text: $nama)
                TextField("Merek", text: $merek)
                TextField("Harga Per Hari", text: $harga)
                #if os(iOS)
                    .keyboardType(.decimalPad)
                #endif
            }
            .navigationTitle("Tambah Mobil")
            .toolbar {
                ToolbarItem(placement: .cancellationAction) {
                    Button("Batal") {
                        presentationMode.wrappedValue.dismiss()
                    }
                }
                ToolbarItem(placement: .confirmationAction) {
                    Button("Simpan") {
                        if let hargaValue = Double(harga) {
                            viewModel.tambahMobil(nama: nama, merek: merek, harga: hargaValue)
                            presentationMode.wrappedValue.dismiss()
                        }
                    }
                }
            }
        }
    }
}

struct AplikasiPenyewaanMobil: App {
    var body: some Scene {
        WindowGroup {
            KontenView()
        }
    }
}
