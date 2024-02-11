project "MyGui"
    kind "SharedLib"
    language "C#"
    location "MyGui"
    targetdir "bin/MyGui/%{cfg.buildcfg}"

    nuget { "Control.Draggable:1.0.5049.269" }

    files { "MyGui/src/**.cs" }
    
    links { "System", "System.Drawing", "System.Windows.Forms" }
