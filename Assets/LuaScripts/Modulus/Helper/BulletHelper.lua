BulletHelper = { }

function BulletHelper:getData(cfgId)
	--bulletconfig 未配置todo
    local bdata = LuaExtend:getBulletData()
    --此处赋值cfg信息
    bdata.luaType = 1
    bdata.cfgId = 1001
    return bdata
end 